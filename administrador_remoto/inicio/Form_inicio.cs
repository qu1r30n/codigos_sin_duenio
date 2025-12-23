using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace inicio
{
    public partial class Form_inicio : Form
    {
        public Form_inicio()
        {
            InitializeComponent();
        }

        private void btn_cliente_Click(object sender, EventArgs e)
        {
            ClienteUI.FormPrincipal client = new ClienteUI.FormPrincipal();
            client.Show();
        }

        private void btn_servidor_Click(object sender, EventArgs e)
        {
            ServidorTCP.inicializar serv = new ServidorTCP.inicializar();
            serv.Show();
        }
    }
}
