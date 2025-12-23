using RemoteCore;
using RemoteCore.Red;
using RemoteCore.Seguridad;
using System;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class FormPrincipal : Form
    {
        private ControladorRemoto controlador;

        public FormPrincipal()
        {
            InitializeComponent();

            // Inicializar controles
            cmbRol.Items.AddRange(new[] { "Admin", "Usuario", "Invitado" });
            cmbRol.SelectedIndex = 2;

            // Crear controlador (UI NO sabe que es TCP internamente)
            controlador = new ControladorRemoto(new ConexionTcp());

            controlador.MensajeSistema += (m) =>
            {
                Invoke(new Action(() => lstEventos.Items.Add(m)));
            };
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.Conectar(txtHost.Text, (int)numPuerto.Value);
                lstEventos.Items.Add("🔌 Conectado al servidor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void btnVerPantalla_Click(object sender, EventArgs e)
        {
            FormPantalla pantalla = new FormPantalla(controlador, false);
            pantalla.Show();
        }


       

        private void btnCompartirPantalla_Click(object sender, EventArgs e)
        {
            FormPantalla pantalla = new FormPantalla(controlador, true);
            pantalla.Show();
        }

    }
}

