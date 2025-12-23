using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServidorTCP
{
    public partial class inicializar : Form
    {
        public inicializar()
        {
            InitializeComponent();
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            // ==========================
            // 🔧 CONFIGURACIÓN DEL SERVIDOR
            // ==========================
            int puerto = Convert.ToInt32(txt_puerto.Text); // 👈 CAMBIA AQUÍ EL PUERTO SI QUIERES

            Servidor servidor = new Servidor(puerto);

            servidor.Iniciar();

            Console.WriteLine("==================================");
            Console.WriteLine(" SERVIDOR TCP INICIADO CORRECTAMENTE");
            Console.WriteLine($" PUERTO: {puerto}");
            Console.WriteLine("==================================");
            Console.WriteLine("Presiona ENTER para cerrar servidor");

            Console.ReadLine(); // mantiene el servidor vivo
        }

    }
}
