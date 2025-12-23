using System.Drawing;
using System.IO;

namespace RemoteCore.Pantalla
{
    /// <summary>
    /// Convierte bytes JPEG en Image
    /// </summary>
    public static class ConvertidorImagen
    {
        public static Image DesdeBytes(byte[] datos)
        {
            using (MemoryStream ms = new MemoryStream(datos))
                return Image.FromStream(ms);
        }
    }
}
