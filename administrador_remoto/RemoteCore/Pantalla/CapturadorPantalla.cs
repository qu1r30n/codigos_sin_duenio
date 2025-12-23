using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace RemoteCore.Pantalla
{
    /// <summary>
    /// Captura la pantalla y la convierte en JPEG comprimido
    /// </summary>
    public class CapturadorPantalla
    {
        public byte[] Capturar(int calidad = 50)
        {
            Rectangle area = Screen.PrimaryScreen.Bounds;
            using (Bitmap bmp = new Bitmap(area.Width, area.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    ImageCodecInfo codec = ObtenerCodec(ImageFormat.Jpeg);
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(Encoder.Quality, calidad);
                    bmp.Save(ms, codec, ep);
                    return ms.ToArray();
                }
            }
        }

        private ImageCodecInfo ObtenerCodec(ImageFormat format)
        {
            foreach (var c in ImageCodecInfo.GetImageDecoders())
                if (c.FormatID == format.Guid) return c;
            return null;
        }
    }
}
