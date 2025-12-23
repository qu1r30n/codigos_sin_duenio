using System;

namespace RemoteCore.Red
{
    /// <summary>
    /// Contrato base para cualquier tipo de conexión (TCP, UDP, WebSocket, etc.)
    /// </summary>
    public interface IConexion
    {
        void Conectar(string host, int puerto);
        void Desconectar();
        void Enviar(byte[] datos);

        event Action<byte[]> DatosRecibidos;
        event Action ConexionCerrada;
    }
}
