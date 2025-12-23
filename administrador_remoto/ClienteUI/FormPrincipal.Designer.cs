namespace ClienteUI
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_contraseña = new System.Windows.Forms.Label();
            this.lbl_rol = new System.Windows.Forms.Label();
            this.lbl_puerto = new System.Windows.Forms.Label();
            this.lbl_host = new System.Windows.Forms.Label();
            this.lstEventos = new System.Windows.Forms.ListBox();
            this.btnCompartirPantalla = new System.Windows.Forms.Button();
            this.btnVerPantalla = new System.Windows.Forms.Button();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.numPuerto = new System.Windows.Forms.NumericUpDown();
            this.txtHost = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPuerto)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_contraseña
            // 
            this.lbl_contraseña.AutoSize = true;
            this.lbl_contraseña.Location = new System.Drawing.Point(23, 200);
            this.lbl_contraseña.Name = "lbl_contraseña";
            this.lbl_contraseña.Size = new System.Drawing.Size(60, 13);
            this.lbl_contraseña.TabIndex = 34;
            this.lbl_contraseña.Text = "contraseña";
            // 
            // lbl_rol
            // 
            this.lbl_rol.AutoSize = true;
            this.lbl_rol.Location = new System.Drawing.Point(21, 145);
            this.lbl_rol.Name = "lbl_rol";
            this.lbl_rol.Size = new System.Drawing.Size(29, 13);
            this.lbl_rol.TabIndex = 33;
            this.lbl_rol.Text = "ROL";
            // 
            // lbl_puerto
            // 
            this.lbl_puerto.AutoSize = true;
            this.lbl_puerto.Location = new System.Drawing.Point(21, 93);
            this.lbl_puerto.Name = "lbl_puerto";
            this.lbl_puerto.Size = new System.Drawing.Size(37, 13);
            this.lbl_puerto.TabIndex = 32;
            this.lbl_puerto.Text = "puerto";
            // 
            // lbl_host
            // 
            this.lbl_host.AutoSize = true;
            this.lbl_host.Location = new System.Drawing.Point(21, 44);
            this.lbl_host.Name = "lbl_host";
            this.lbl_host.Size = new System.Drawing.Size(27, 13);
            this.lbl_host.TabIndex = 31;
            this.lbl_host.Text = "host";
            // 
            // lstEventos
            // 
            this.lstEventos.FormattingEnabled = true;
            this.lstEventos.Location = new System.Drawing.Point(211, 12);
            this.lstEventos.Name = "lstEventos";
            this.lstEventos.Size = new System.Drawing.Size(120, 407);
            this.lstEventos.TabIndex = 30;
            // 
            // btnCompartirPantalla
            // 
            this.btnCompartirPantalla.Location = new System.Drawing.Point(105, 343);
            this.btnCompartirPantalla.Name = "btnCompartirPantalla";
            this.btnCompartirPantalla.Size = new System.Drawing.Size(75, 23);
            this.btnCompartirPantalla.TabIndex = 29;
            this.btnCompartirPantalla.Text = "Compartir pantalla";
            this.btnCompartirPantalla.UseVisualStyleBackColor = true;
            this.btnCompartirPantalla.Click += new System.EventHandler(this.btnCompartirPantalla_Click);
            // 
            // btnVerPantalla
            // 
            this.btnVerPantalla.Location = new System.Drawing.Point(24, 343);
            this.btnVerPantalla.Name = "btnVerPantalla";
            this.btnVerPantalla.Size = new System.Drawing.Size(75, 23);
            this.btnVerPantalla.TabIndex = 28;
            this.btnVerPantalla.Text = "Ver pantalla";
            this.btnVerPantalla.UseVisualStyleBackColor = true;
            this.btnVerPantalla.Click += new System.EventHandler(this.btnVerPantalla_Click);
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(105, 284);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(75, 23);
            this.btnDesconectar.TabIndex = 27;
            this.btnDesconectar.Text = "Desconectar";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(24, 284);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 26;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(24, 225);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 25;
            this.txtClave.Text = "admin";
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(24, 161);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(121, 21);
            this.cmbRol.TabIndex = 24;
            // 
            // numPuerto
            // 
            this.numPuerto.Location = new System.Drawing.Point(24, 109);
            this.numPuerto.Name = "numPuerto";
            this.numPuerto.Size = new System.Drawing.Size(120, 20);
            this.numPuerto.TabIndex = 23;
            this.numPuerto.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(24, 60);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 20);
            this.txtHost.TabIndex = 22;
            this.txtHost.Text = "127.0.0.1";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 450);
            this.Controls.Add(this.lbl_contraseña);
            this.Controls.Add(this.lbl_rol);
            this.Controls.Add(this.lbl_puerto);
            this.Controls.Add(this.lbl_host);
            this.Controls.Add(this.lstEventos);
            this.Controls.Add(this.btnCompartirPantalla);
            this.Controls.Add(this.btnVerPantalla);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.cmbRol);
            this.Controls.Add(this.numPuerto);
            this.Controls.Add(this.txtHost);
            this.Name = "FormPrincipal";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numPuerto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_contraseña;
        private System.Windows.Forms.Label lbl_rol;
        private System.Windows.Forms.Label lbl_puerto;
        private System.Windows.Forms.Label lbl_host;
        private System.Windows.Forms.ListBox lstEventos;
        private System.Windows.Forms.Button btnCompartirPantalla;
        private System.Windows.Forms.Button btnVerPantalla;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.NumericUpDown numPuerto;
        private System.Windows.Forms.TextBox txtHost;
    }
}

