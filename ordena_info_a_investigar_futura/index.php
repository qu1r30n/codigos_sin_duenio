<?php

// --------------------
// ARCHIVO DINÁMICO
// --------------------

// Obtenemos el nombre del archivo desde POST o GET. Si no se envía, se usa 'datos.txt' por defecto.
$nombreArchivo = $_POST['archivo'] ?? $_GET['archivo'] ?? 'datos.txt';

// Limpiamos el nombre del archivo para evitar rutas no deseadas
$nombreArchivo = basename($nombreArchivo);

// Si el archivo no existe, lo creamos vacío
if (!file_exists($nombreArchivo)) {
    file_put_contents($nombreArchivo, "");
}

// --------------------
// FUNCIONES
// --------------------

// Función para leer datos desde el archivo y retornarlos como un arreglo asociativo
function leerDatos($archivo) {
    // Leemos todas las líneas del archivo ignorando saltos de línea
    $lineas = file($archivo, FILE_IGNORE_NEW_LINES);
    $datos = [];

    foreach ($lineas as $linea) {
        // Solo procesamos líneas que no estén vacías
        if (!empty($linea)) {
            // Dividimos la línea por el separador "|" en id, título, URL y texto
            list($id, $titulo, $url, $texto) = explode("|", $linea);
            $datos[] = [
                'id' => (float)$id, // convertimos ID a número decimal
                'titulo' => $titulo,
                'url' => $url,
                'texto' => base64_decode($texto) // decodificamos el texto de base64
            ];
        }
    }
    return $datos;
}

// Función para guardar datos en el archivo, sobrescribiendo el contenido
function guardarDatos($archivo, $datos) {
    // Ordenamos los datos por ID de menor a mayor
    usort($datos, function($a, $b) {
        return $a['id'] <=> $b['id'];
    });

    $contenido = "";
    foreach ($datos as $dato) {
        // Convertimos cada registro en una línea con formato: id|titulo|url|texto_en_base64
        $contenido .= $dato['id'] . "|" .
                      $dato['titulo'] . "|" .
                      $dato['url'] . "|" .
                      base64_encode($dato['texto']) . "\n";
    }

    // Guardamos el contenido en el archivo
    file_put_contents($archivo, $contenido);
}

// Función para obtener el mínimo y máximo ID del arreglo de datos
function obtenerMinMax($datos) {
    if (empty($datos)) return [0, 0];
    $ids = array_column($datos, 'id');
    return [min($ids), max($ids)];
}

// Leemos los datos actuales del archivo
$datos = leerDatos($nombreArchivo);
$error = ""; // variable para almacenar errores
$mostrarArchivo = false; // bandera para mostrar el archivo completo (no se usa en este fragmento)


// --------------------
// GUARDAR NUEVO REGISTRO
// --------------------
if (isset($_POST['guardar'])) {
    $titulo = $_POST['titulo'] ?? "";
    $url = $_POST['url'] ?? "";
    $texto = $_POST['texto'] ?? "";
    $idInput = trim($_POST['id']); // id ingresado por el usuario

    // Obtenemos el ID mínimo y máximo actuales
    list($minId, $maxId) = obtenerMinMax($datos);

    if ($idInput === "") {
        // Si no se especifica ID, se asigna uno automático según posición
        $nuevoId = ($_POST['posicion'] == "inicio") ? $minId - 1 : $maxId + 1;
    } else {
        $nuevoId = (float)$idInput;
        // Verificamos que no exista ya el ID
        foreach ($datos as $dato) {
            if ($dato['id'] == $nuevoId) {
                $error = "⚠️ Ese ID ya existe.";
                break;
            }
        }
    }

    // Si no hay error, agregamos el nuevo registro
    if (!$error) {
        $datos[] = [
            'id' => $nuevoId,
            'titulo' => $titulo,
            'url' => $url,
            'texto' => $texto
        ];

        guardarDatos($nombreArchivo, $datos);
        // Redirigimos para evitar resubmisión de formulario
        header("Location: ?archivo=" . $nombreArchivo);
        exit;
    }
}

// --------------------
// ELIMINAR REGISTRO
// --------------------
if (isset($_GET['eliminar'])) {
    $idEliminar = (float)$_GET['eliminar'];

    // Filtramos el arreglo eliminando el registro con ese ID
    $datos = array_filter($datos, function ($d) use ($idEliminar) {
        return $d['id'] != $idEliminar;
    });

    // Guardamos cambios y redirigimos
    guardarDatos($nombreArchivo, array_values($datos));
    header("Location: ?archivo=" . $nombreArchivo);
    exit;
}

// --------------------
// EDITAR REGISTRO
// --------------------
$editar = null;

if (isset($_GET['editar'])) {
    foreach ($datos as $dato) {
        if ($dato['id'] == (float)$_GET['editar']) {
            $editar = $dato; // cargamos datos del registro a editar
            break;
        }
    }
}

// --------------------
// ACTUALIZAR REGISTRO EXISTENTE
// --------------------
if (isset($_POST['actualizar'])) {
    $idOriginal = (float)$_POST['id_original']; // id anterior
    $idInput = trim($_POST['id']); // id nuevo ingresado

    list($minId, $maxId) = obtenerMinMax($datos);

    if ($idInput === "") {
        // ID automático si no se ingresa
        $nuevoId = ($_POST['posicion'] == "inicio") ? $minId - 1 : $maxId + 1;
    } else {
        $nuevoId = (float)$idInput;
        // Comprobamos que el nuevo ID no exista en otro registro
        foreach ($datos as $dato) {
            if ($dato['id'] == $nuevoId && $dato['id'] != $idOriginal) {
                $error = "⚠️ Ese ID ya existe.";
                break;
            }
        }
    }

    // Actualizamos datos si no hay error
    if (!$error) {
        foreach ($datos as &$dato) {
            if ($dato['id'] == $idOriginal) {
                $dato['id'] = $nuevoId;
                $dato['titulo'] = $_POST['titulo'] ?? "";
                $dato['url'] = $_POST['url'] ?? "";
                $dato['texto'] = $_POST['texto'] ?? "";
            }
        }

        guardarDatos($nombreArchivo, $datos);
        header("Location: ?archivo=" . $nombreArchivo);
        exit;
    }
}

// Volvemos a leer los datos actualizados
$datos = leerDatos($nombreArchivo);
?>

<!-- -------------------- -->
<!-- INTERFAZ HTML -->
<!-- -------------------- -->

<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Gestor TXT</title>
<style>
/* Estilos básicos de la página y formularios */
body { font-family: Arial; margin:20px; }
.form-row { display:flex; gap:10px; margin-bottom:8px; }
.form-row input, .form-row select { padding:5px; }
textarea { width:100%; height:60px; resize:vertical; }
table { border-collapse: collapse; width:100%; margin-top:20px; }
th, td { border:1px solid #ccc; padding:6px; font-size:14px; }
th { background:#f0f0f0; }
.small { width:120px; }
</style>
</head>
<body>

<!-- FORMULARIO DE CREACIÓN / EDICIÓN -->
<form method="POST">

<!-- FILA 1: Campos de archivo, ID, posición, título, URL y botones -->
<div class="form-row">
    <input class="small" type="text" name="archivo" value="<?php echo htmlspecialchars($nombreArchivo); ?>">

    <?php if($editar): ?>
        <!-- Guardamos el ID original si estamos editando -->
        <input type="hidden" name="id_original" value="<?php echo $editar['id']; ?>">
    <?php endif; ?>

    <input class="small" type="number" step="0.01" name="id" placeholder="ID" value="<?php echo $editar['id'] ?? ''; ?>">

    <select name="posicion" class="small">
        <option value="final">Final</option>
        <option value="inicio">Inicio</option>
    </select>

    <input type="text" name="titulo" placeholder="Título" value="<?php echo $editar['titulo'] ?? ''; ?>">

    <input type="text" name="url" placeholder="URL" value="<?php echo $editar['url'] ?? ''; ?>">

    <?php if($editar): ?>
        <button name="actualizar">Actualizar</button>
        <a href="?archivo=<?php echo $nombreArchivo; ?>">Cancelar</a>
    <?php else: ?>
        <button name="guardar">Guardar</button>
        <button name="ver_archivo">Ver archivo</button>
    <?php endif; ?>
</div>

<!-- FILA 2: Área de texto -->
<div class="form-row">
    <textarea name="texto" placeholder="Texto..."><?php echo $editar['texto'] ?? ''; ?></textarea>
</div>

</form>

<!-- MOSTRAR MENSAJE DE ERROR -->
<?php if($error): ?>
<p style="color:red;"><?php echo $error; ?></p>
<?php endif; ?>

<!-- TABLA DE DATOS -->
<table>
<tr>
    <th>ID</th>
    <th>Título</th>
    <th>URL</th>
    <th>Texto</th>
    <th></th>
</tr>

<?php foreach($datos as $d): ?>
<tr>
    <td><?php echo $d['id']; ?></td>
    <td><?php echo htmlspecialchars($d['titulo']); ?></td>
    <td>
        <?php if($d['url']): ?>
            <a href="<?php echo htmlspecialchars($d['url']); ?>" target="_blank">Abrir</a>
        <?php endif; ?>
    </td>
    <td><?php echo nl2br(htmlspecialchars($d['texto'])); ?></td>
    <td>
        <a href="?editar=<?php echo $d['id']; ?>&archivo=<?php echo $nombreArchivo; ?>">Editar</a>
        |
        <a href="?eliminar=<?php echo $d['id']; ?>&archivo=<?php echo $nombreArchivo; ?>" onclick="return confirm('Eliminar?')">X</a>
    </td>
</tr>
<?php endforeach; ?>
</table>

</body>
</html>