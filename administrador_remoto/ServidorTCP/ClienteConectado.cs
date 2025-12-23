using System.IO;
using System.Net.Sockets;


namespace ServidorTCP
{
    internal class ClienteConectado
    {
        public TcpClient Tcp;
        public NetworkStream Stream;
        public BinaryReader Reader;
        public BinaryWriter Writer;
        public string Rol;

        public ClienteConectado(TcpClient tcp)
        {
            Tcp = tcp;
            Stream = tcp.GetStream();
            Reader = new BinaryReader(Stream);
            Writer = new BinaryWriter(Stream);
            Rol = "invitado";
        }
    }
}
