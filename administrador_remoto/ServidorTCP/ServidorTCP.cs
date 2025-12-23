using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServidorTCP
{
    /// <summary>
    /// Servidor TCP puro (sin NuGet)
    /// </summary>
    public class Servidor
    {
        private TcpListener listener;
        private List<ClienteConectado> clientes;
        private bool activo = false;

        // ==========================
        // 🔑 CLAVES DE ACCESO
        // ==========================
        private const string CLAVE_ADMIN = "admin";
        private const string CLAVE_USUARIO = "usuario";
        private const string CLAVE_INVITADO = "invitado";

        public Servidor(int puerto)
        {
            listener = new TcpListener(IPAddress.Any, puerto);
            clientes = new List<ClienteConectado>();
        }

        /// <summary>
        /// Inicia el servidor
        /// </summary>
        public void Iniciar()
        {
            activo = true;
            listener.Start();
            new Thread(AceptarClientes) { IsBackground = true }.Start();
        }

        /// <summary>
        /// Acepta clientes entrantes
        /// </summary>
        private void AceptarClientes()
        {
            while (activo)
            {
                TcpClient tcp = listener.AcceptTcpClient();
                ClienteConectado cliente = new ClienteConectado(tcp);

                lock (clientes)
                    clientes.Add(cliente);

                Console.WriteLine("🔗 Cliente conectado");

                new Thread(() => ManejarCliente(cliente)) { IsBackground = true }.Start();
            }
        }

        /// <summary>
        /// Maneja la comunicación de un cliente
        /// </summary>
        private void ManejarCliente(ClienteConectado cliente)
        {
            try
            {
                while (true)
                {
                    byte tipo = cliente.Reader.ReadByte();

                    // ======================
                    // 1️⃣ LOGIN
                    // ======================
                    if (tipo == 1)
                    {
                        string clave = cliente.Reader.ReadString();
                        cliente.Rol = ObtenerRol(clave);

                        cliente.Writer.Write(cliente.Rol);
                        cliente.Writer.Flush();

                        Console.WriteLine($"🔑 Cliente autenticado como {cliente.Rol}");
                    }

                    // ======================
                    // 2️⃣ PANTALLA
                    // ======================
                    if (tipo == 2)
                    {
                        int longitud = cliente.Reader.ReadInt32();
                        byte[] datos = cliente.Reader.ReadBytes(longitud);
                        ReenviarPantalla(cliente, datos);
                    }

                    // ======================
                    // 3️⃣ RATÓN
                    // ======================
                    if (tipo == 3 && PuedeControlar(cliente))
                    {
                        string evento = cliente.Reader.ReadString();
                        int x = cliente.Reader.ReadInt32();
                        int y = cliente.Reader.ReadInt32();
                        int boton = cliente.Reader.ReadInt32();
                        ReenviarRaton(cliente, evento, x, y, boton);
                    }

                    // ======================
                    // 4️⃣ TECLADO
                    // ======================
                    if (tipo == 4 && PuedeControlar(cliente))
                    {
                        string evento = cliente.Reader.ReadString();
                        int tecla = cliente.Reader.ReadInt32();
                        ReenviarTeclado(cliente, evento, tecla);
                    }
                }
            }
            catch
            {
                Console.WriteLine("❌ Cliente desconectado");

                lock (clientes)
                    clientes.Remove(cliente);
            }
        }

        // ======================
        // 🔒 PERMISOS
        // ======================
        private bool PuedeControlar(ClienteConectado c)
        {
            return c.Rol == "admin" || c.Rol == "usuario";
        }

        private string ObtenerRol(string clave)
        {
            if (clave == CLAVE_ADMIN) return "admin";
            if (clave == CLAVE_USUARIO) return "usuario";
            return "invitado";
        }

        // ======================
        // 🔁 REENVÍOS
        // ======================
        private void ReenviarPantalla(ClienteConectado origen, byte[] datos)
        {
            lock (clientes)
            {
                foreach (var c in clientes)
                {
                    if (c != origen)
                    {
                        c.Writer.Write((byte)2);
                        c.Writer.Write(datos.Length);
                        c.Writer.Write(datos);
                        c.Writer.Flush();
                    }
                }
            }
        }

        private void ReenviarRaton(ClienteConectado origen, string tipo, int x, int y, int boton)
        {
            lock (clientes)
            {
                foreach (var c in clientes)
                {
                    if (c != origen)
                    {
                        c.Writer.Write((byte)3);
                        c.Writer.Write(tipo);
                        c.Writer.Write(x);
                        c.Writer.Write(y);
                        c.Writer.Write(boton);
                        c.Writer.Flush();
                    }
                }
            }
        }

        private void ReenviarTeclado(ClienteConectado origen, string tipo, int tecla)
        {
            lock (clientes)
            {
                foreach (var c in clientes)
                {
                    if (c != origen)
                    {
                        c.Writer.Write((byte)4);
                        c.Writer.Write(tipo);
                        c.Writer.Write(tecla);
                        c.Writer.Flush();
                    }
                }
            }
        }
    }
}
