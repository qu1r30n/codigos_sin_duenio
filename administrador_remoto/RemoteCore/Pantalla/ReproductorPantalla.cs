using System.Drawing;
using System.IO;

namespace RemoteCore.Pantalla
{
    /// <summary>
    /// Convierte datos de imagen recibidos (byte[]) en Image
    /// NO dibuja, solo transforma
    /// </summary>
    public class ReproductorPantalla
    {
        /// <summary>
        /// Convierte bytes JPEG/PNG a Image
        /// </summary>
        public Image Convertir(byte[] datos)
        {
            if (datos == null || datos.Length == 0)
                return null;

            // Se crea un MemoryStream SOLO para leer
            using (MemoryStream ms = new MemoryStream(datos))
            {
                // Clonamos la imagen para evitar que el stream se libere
                using (Image imgTemp = Image.FromStream(ms))
                {
                    return new Bitmap(imgTemp);
                }
            }
        }
    }
}
