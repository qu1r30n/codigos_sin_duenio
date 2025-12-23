using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace RemoteCore.Red
{
    /// <summary>
    /// Implementación TCP pura (sin NuGet)
    /// </summary>
    public class ConexionTcp : IConexion
    {
        private TcpClient cliente;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private Thread hiloLectura;
        private bool activo;

        public event Action<byte[]> DatosRecibidos;
        public event Action ConexionCerrada;

        public void Conectar(string host, int puerto)
        {
            cliente = new TcpClient();
            cliente.Connect(host, puerto);

            stream = cliente.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

            activo = true;
            hiloLectura = new Thread(LeerLoop) { IsBackground = true };
            hiloLectura.Start();
        }

        public void Enviar(byte[] datos)
        {
            if (!activo) return;

            //writer.Write(datos.Length);
            writer.Write(datos);
            writer.Flush();
        }

        private void LeerLoop()
        {
            try
            {
                while (activo)
                {
                    byte tipo = reader.ReadByte();
                    int len = reader.ReadInt32();
                    byte[] datos = reader.ReadBytes(len);

                    using (MemoryStream ms = new MemoryStream())
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        bw.Write(tipo);
                        bw.Write(len);
                        bw.Write(datos);
                        DatosRecibidos?.Invoke(ms.ToArray()); // 🔥 AHORA SÍ
                    }
                }
            }
            catch
            {
                activo = false;
                ConexionCerrada?.Invoke();
            }
        }




        public void Desconectar()
        {
            activo = false;
            try { cliente?.Close(); } catch { }
        }
    }
}
