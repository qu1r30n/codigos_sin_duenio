using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteCore.Red
{
    /// <summary>
    /// Tipos de paquetes que viajan por la red
    /// </summary>
    public enum TipoPaquete : byte
    {
        Login = 1,
        Pantalla = 2,
        Raton = 3,
        Teclado = 4,
        Mensaje = 5
    }
}
