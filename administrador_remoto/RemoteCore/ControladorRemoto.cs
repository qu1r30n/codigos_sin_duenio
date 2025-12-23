using RemoteCore.Control;
using RemoteCore.Pantalla;
using RemoteCore.Red;
using RemoteCore.Seguridad;
using System;
using System.IO;


namespace RemoteCore
{
    /// <summary>
    /// Orquestador principal (la UI solo habla con esto)
    /// </summary>
    public class ControladorRemoto
    {
        private readonly IConexion conexion;
        private readonly CapturadorPantalla capturador;

        public RolUsuario RolActual { get; private set; } = RolUsuario.Invitado;

        public event Action<byte[]> PantallaRecibida;
        public event Action<string> MensajeSistema;


        public void Conectar(string host, int puerto)
        {
            conexion.Conectar(host, puerto);
        }

        /*codigo anterior
        public ControladorRemoto(IConexion conexion)
        {
            this.conexion = conexion;
            capturador = new CapturadorPantalla();

            conexion.DatosRecibidos += ProcesarDatos;
            conexion.ConexionCerrada += () => MensajeSistema?.Invoke("Conexión cerrada");
        }
        

        public void EnviarPantalla()
        {
            byte[] img = capturador.Capturar();
            conexion.Enviar(img);
        }
        


        private void ProcesarDatos(byte[] datos)
        {
            // En esta versión simple asumimos que todo lo recibido es pantalla
            PantallaRecibida?.Invoke(datos);
        }
        */

        



        public ControladorRemoto(IConexion conexion)
        {
            this.conexion = conexion;
            capturador = new CapturadorPantalla();

            conexion.DatosRecibidos += ProcesarDatos;
        }

        public void EnviarPantalla()
        {
            byte[] imagen = capturador.Capturar();

            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write((byte)TipoPaquete.Pantalla);
                bw.Write(imagen.Length);
                bw.Write(imagen);

                conexion.Enviar(ms.ToArray());
            }
        }

        private void ProcesarDatos(byte[] datos)
        {
            using (MemoryStream ms = new MemoryStream(datos))
            using (BinaryReader br = new BinaryReader(ms))
            {
                TipoPaquete tipo = (TipoPaquete)br.ReadByte();

                if (tipo == TipoPaquete.Pantalla)
                {
                    int len = br.ReadInt32();
                    byte[] img = br.ReadBytes(len);
                    PantallaRecibida?.Invoke(img);
                }
            }
        }




    }
}
