using RemoteCore;
using RemoteCore.Control;
using RemoteCore.Pantalla;
using RemoteCore.Red;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class FormPantalla : Form
    {
        private readonly ControladorRemoto controlador;
        private readonly bool compartir;

        public FormPantalla(ControladorRemoto controlador, bool compartir)
        {
            InitializeComponent();
            this.controlador = controlador;
            this.compartir = compartir;

            KeyPreview = true;

            lblEstado.Text = compartir ? "📡 Compartiendo pantalla" : "👀 Visualizando pantalla";

            // Recibir imagen desde núcleo
            controlador.PantallaRecibida += MostrarPantalla;


            if (compartir)
                timerPantalla.Start();
        }

        /*cod_ant MostrarPantalla 
        private void MostrarPantalla(byte[] datos)
        {
            Image img = ConvertidorImagen.DesdeBytes(datos);
            if (picPantalla.InvokeRequired)
                picPantalla.Invoke(new Action(() => picPantalla.Image = img));
            else
                picPantalla.Image = img;
        }
        */

        private ReproductorPantalla reproductor = new ReproductorPantalla();
        private void MostrarPantalla(byte[] datos)
        {
            Image img = reproductor.Convertir(datos);

            if (img == null) return;

            if (picPantalla.InvokeRequired)
                picPantalla.Invoke(new Action(() => picPantalla.Image = img));
            else
                picPantalla.Image = img;
        }

        private void timerPantalla_Tick(object sender, EventArgs e)
        {
            // UI SOLO LLAMA
            controlador.EnviarPantalla();
        }

        // =========================
        // CONTROL REMOTO
        // =========================
        private void picPantalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (!chkControl.Checked) return;
            InputRemoto.MoverRaton(e.X, e.Y);
        }

        private void picPantalla_MouseDown(object sender, MouseEventArgs e)
        {
            if (!chkControl.Checked) return;
            InputRemoto.ClickRaton(true, e.Button == MouseButtons.Left);
        }

        private void picPantalla_MouseUp(object sender, MouseEventArgs e)
        {
            if (!chkControl.Checked) return;
            InputRemoto.ClickRaton(false, e.Button == MouseButtons.Left);
        }

        private void FormPantalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (!chkControl.Checked) return;
            InputRemoto.Tecla((byte)e.KeyValue, true);
        }

        private void FormPantalla_KeyUp(object sender, KeyEventArgs e)
        {
            if (!chkControl.Checked) return;
            InputRemoto.Tecla((byte)e.KeyValue, false);
        }

    }
}

