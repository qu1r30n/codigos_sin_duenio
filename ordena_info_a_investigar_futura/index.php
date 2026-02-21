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

/* GUARDAR */
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

/* ELIMINAR */
if (isset($_GET['eliminar'])) {

    $idEliminar = (float)$_GET['eliminar'];

    $datos = array_filter($datos, function ($d) use ($idEliminar) {
        return $d['id'] != $idEliminar;
    });

    guardarDatos($nombreArchivo, array_values($datos));
    header("Location: ?archivo=" . $nombreArchivo);
    exit;
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
.form-row { display:flex; gap:10px; margin-bottom:8px; flex-wrap:wrap; }
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

.preview video,
.preview iframe,
.preview img {
    max-width:250px;
    margin-top:5px;
}
</style>
</head>
<body>

<form method="POST">
<div class="form-row">

<input type="text" name="archivo" value="<?php echo htmlspecialchars($nombreArchivo); ?>">
<input type="number" step="0.01" name="id" placeholder="ID">
<select name="posicion">
<option value="final">Final</option>
<option value="inicio">Inicio</option>
</select>
<input type="text" name="titulo" placeholder="Título">
<input type="text" name="url" placeholder="URL">
<button name="guardar">Guardar</button>

</div>

<div class="form-row">
<textarea name="texto" placeholder="Texto (base64)..."></textarea>
</div>

<div class="form-row">
<textarea name="texto_sin_codificar" placeholder="Texto sin codificar..."></textarea>
</div>

</form>

<table>
<tr>
<th>ID</th>
<th>Título</th>
<th>URL / Vista previa</th>
<th>Texto</th>
<th>Texto sin codificar</th>
<th>Acciones</th>
</tr>

<?php foreach($datos as $d): ?>
<tr>

<td><?php echo $d['id']; ?></td>
<td><?php echo htmlspecialchars($d['titulo']); ?></td>

<td class="preview">

<?php if($d['url']): 
$url = htmlspecialchars($d['url']);
?>

<a href="<?php echo $url; ?>" target="_blank">Abrir</a><br>

<?php
if (preg_match('/youtube\.com|youtu\.be/', $url)) {

    if (strpos($url, 'watch?v=') !== false) {
        $videoId = explode('watch?v=', $url)[1];
        $videoId = explode('&', $videoId)[0];
    } elseif (strpos($url, 'youtu.be/') !== false) {
        $videoId = explode('youtu.be/', $url)[1];
    }

    echo '<iframe width="250" height="150"
        src="https://www.youtube.com/embed/'.$videoId.'"
        frameborder="0" allowfullscreen></iframe>';

}
elseif (preg_match('/\.(mp4|webm|ogg)$/i', $url)) {

    echo '<video controls>
            <source src="'.$url.'">
          </video>';

}
elseif (preg_match('/\.(jpg|jpeg|png|gif|webp)$/i', $url)) {

    echo '<img src="'.$url.'">';

}
?>

<?php endif; ?>

</td>

<td>
<div class="scrollbox">
<?php echo htmlspecialchars($d['texto']); ?>
</div>
</td>

<td>
<div class="scrollbox">
<?php echo htmlspecialchars($d['texto_sin_codificar']); ?>
</div>
</td>

<td>
<a href="?eliminar=<?php echo $d['id']; ?>&archivo=<?php echo $nombreArchivo; ?>" onclick="return confirm('Eliminar?')">Eliminar</a>
</td>

</tr>
<?php endforeach; ?>

</table>

</body>
</html>