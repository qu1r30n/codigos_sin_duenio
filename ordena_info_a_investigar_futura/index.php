<?php

$nombreArchivo = $_POST['archivo'] ?? $_GET['archivo'] ?? 'datos.txt';
$nombreArchivo = basename($nombreArchivo);

if (!file_exists($nombreArchivo)) {
    file_put_contents($nombreArchivo, "");
}

function leerDatos($archivo) {
    $lineas = file($archivo, FILE_IGNORE_NEW_LINES);
    $datos = [];

    foreach ($lineas as $linea) {
        if (!empty($linea)) {

            $partes = explode("|", $linea);

            $id = $partes[0] ?? "";
            $titulo = $partes[1] ?? "";
            $url = $partes[2] ?? "";
            $texto = $partes[3] ?? "";
            $texto_sin_codificar = $partes[4] ?? "";

            $datos[] = [
                'id' => (float)$id,
                'titulo' => $titulo,
                'url' => $url,
                'texto' => base64_decode($texto),
                'texto_sin_codificar' => $texto_sin_codificar
            ];
        }
    }

    return $datos;
}

function guardarDatos($archivo, $datos) {

    usort($datos, function($a, $b) {
        return $a['id'] <=> $b['id'];
    });

    $contenido = "";

    foreach ($datos as $dato) {

        $textoPlano = str_replace(["\n", "\r", "|"], " ", $dato['texto_sin_codificar']);

        $contenido .= $dato['id'] . "|" .
                      $dato['titulo'] . "|" .
                      $dato['url'] . "|" .
                      base64_encode($dato['texto']) . "|" .
                      $textoPlano . "\n";
    }

    file_put_contents($archivo, $contenido);
}

function obtenerMinMax($datos) {
    if (empty($datos)) return [0, 0];
    $ids = array_column($datos, 'id');
    return [min($ids), max($ids)];
}

$datos = leerDatos($nombreArchivo);
$error = "";
$editar = null;

// --------------------
// GUARDAR
// --------------------

if (isset($_POST['guardar'])) {

    $titulo = $_POST['titulo'] ?? "";
    $url = $_POST['url'] ?? "";
    $texto = $_POST['texto'] ?? "";
    $texto_sin_codificar = $_POST['texto_sin_codificar'] ?? "";
    $idInput = trim($_POST['id']);

    list($minId, $maxId) = obtenerMinMax($datos);

    if ($idInput === "") {
        $nuevoId = ($_POST['posicion'] == "inicio") ? $minId - 1 : $maxId + 1;
    } else {
        $nuevoId = (float)$idInput;
        foreach ($datos as $dato) {
            if ($dato['id'] == $nuevoId) {
                $error = "⚠️ Ese ID ya existe.";
                break;
            }
        }
    }

    if (!$error) {

        $datos[] = [
            'id' => $nuevoId,
            'titulo' => $titulo,
            'url' => $url,
            'texto' => $texto,
            'texto_sin_codificar' => $texto_sin_codificar
        ];

        guardarDatos($nombreArchivo, $datos);
        header("Location: ?archivo=" . $nombreArchivo);
        exit;
    }
}

// --------------------
// ELIMINAR
// --------------------

if (isset($_GET['eliminar'])) {

    $idEliminar = (float)$_GET['eliminar'];

    $datos = array_filter($datos, function ($d) use ($idEliminar) {
        return $d['id'] != $idEliminar;
    });

    guardarDatos($nombreArchivo, array_values($datos));
    header("Location: ?archivo=" . $nombreArchivo);
    exit;
}

// --------------------
// EDITAR
// --------------------

if (isset($_GET['editar'])) {
    foreach ($datos as $dato) {
        if ($dato['id'] == (float)$_GET['editar']) {
            $editar = $dato;
            break;
        }
    }
}

// --------------------
// ACTUALIZAR
// --------------------

if (isset($_POST['actualizar'])) {

    $idOriginal = (float)$_POST['id_original'];
    $idInput = trim($_POST['id']);

    list($minId, $maxId) = obtenerMinMax($datos);

    if ($idInput === "") {
        $nuevoId = ($_POST['posicion'] == "inicio") ? $minId - 1 : $maxId + 1;
    } else {
        $nuevoId = (float)$idInput;
        foreach ($datos as $dato) {
            if ($dato['id'] == $nuevoId && $dato['id'] != $idOriginal) {
                $error = "⚠️ Ese ID ya existe.";
                break;
            }
        }
    }

    if (!$error) {

        foreach ($datos as &$dato) {
            if ($dato['id'] == $idOriginal) {
                $dato['id'] = $nuevoId;
                $dato['titulo'] = $_POST['titulo'] ?? "";
                $dato['url'] = $_POST['url'] ?? "";
                $dato['texto'] = $_POST['texto'] ?? "";
                $dato['texto_sin_codificar'] = $_POST['texto_sin_codificar'] ?? "";
            }
        }

        guardarDatos($nombreArchivo, $datos);
        header("Location: ?archivo=" . $nombreArchivo);
        exit;
    }
}

$datos = leerDatos($nombreArchivo);
?>

<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8">
<title>Gestor TXT</title>

<style>
body { font-family: Arial; margin:20px; }
.form-row { display:flex; gap:10px; margin-bottom:8px; }
.form-row input, .form-row select { padding:5px; }
textarea { width:100%; height:120px; resize:vertical; }

table { border-collapse: collapse; width:100%; margin-top:20px; table-layout: fixed; }
th, td { border:1px solid #ccc; padding:6px; font-size:14px; vertical-align:top; }
th { background:#f0f0f0; }

.scrollbox { 
    max-height:120px;
    overflow:auto;
    white-space:pre-wrap;
    word-break:break-word;
    padding:5px;
}

.copy-btn {
    margin-top:5px;
    font-size:12px;
    padding:2px 6px;
    cursor:pointer;
}
</style>
</head>
<body>

<form method="POST">

<div class="form-row">

<input type="text" name="archivo" value="<?php echo htmlspecialchars($nombreArchivo); ?>">

<?php if($editar): ?>
<input type="hidden" name="id_original" value="<?php echo $editar['id']; ?>">
<?php endif; ?>

<input type="number" step="0.01" name="id" placeholder="ID" value="<?php echo $editar['id'] ?? ''; ?>">

<select name="posicion">
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
<?php endif; ?>

</div>

<div class="form-row">
<textarea name="texto" placeholder="Texto (base64)..."><?php echo $editar['texto'] ?? ''; ?></textarea>
</div>

<div class="form-row">
<textarea name="texto_sin_codificar" placeholder="Texto sin codificar..."><?php echo $editar['texto_sin_codificar'] ?? ''; ?></textarea>
</div>

</form>

<?php if($error): ?>
<p style="color:red;"><?php echo $error; ?></p>
<?php endif; ?>

<table>
<tr>
<th>ID</th>
<th>Título</th>
<th>URL</th>
<th>Texto</th>
<th>Texto sin codificar</th>
<th>Acciones</th>
</tr>

<?php foreach($datos as $d): ?>
<tr>

<td><?php echo $d['id']; ?></td>
<td><?php echo htmlspecialchars($d['titulo']); ?></td>
<td><?php if($d['url']): ?><a href="<?php echo htmlspecialchars($d['url']); ?>" target="_blank">Abrir</a><?php endif; ?></td>

<td>
<div class="scrollbox" id="texto_<?php echo $d['id']; ?>">
<?php echo htmlspecialchars($d['texto']); ?>
</div>
<button class="copy-btn" onclick="copiarCelda('texto_<?php echo $d['id']; ?>')">Copiar</button>
</td>

<td>
<div class="scrollbox" id="texto2_<?php echo $d['id']; ?>">
<?php echo htmlspecialchars($d['texto_sin_codificar']); ?>
</div>
<button class="copy-btn" onclick="copiarCelda('texto2_<?php echo $d['id']; ?>')">Copiar</button>
</td>

<td>
<a href="?editar=<?php echo $d['id']; ?>&archivo=<?php echo $nombreArchivo; ?>">Editar</a><br>
<a href="?eliminar=<?php echo $d['id']; ?>&archivo=<?php echo $nombreArchivo; ?>" onclick="return confirm('Eliminar?')">Eliminar</a>
</td>

</tr>
<?php endforeach; ?>

</table>

<script>
function copiarCelda(id) {
    const texto = document.getElementById(id).innerText;
    navigator.clipboard.writeText(texto).then(() => {
        alert("Contenido copiado");
    });
}
</script>

</body>
</html>