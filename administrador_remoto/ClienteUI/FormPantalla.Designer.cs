namespace ClienteUI
{
    partial class FormPantalla
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chkControl = new System.Windows.Forms.CheckBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.picPantalla = new System.Windows.Forms.PictureBox();
            this.timerPantalla = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picPantalla)).BeginInit();
            this.SuspendLayout();
            // 
            // chkControl
            // 
            this.chkControl.AutoSize = true;
            this.chkControl.Location = new System.Drawing.Point(605, 130);
            this.chkControl.Name = "chkControl";
            this.chkControl.Size = new System.Drawing.Size(134, 17);
            this.chkControl.TabIndex = 6;
            this.chkControl.Text = "Habilitar control remoto";
            this.chkControl.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(602, 169);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 5;
            this.lblEstado.Text = "Estado";
            // 
            // picPantalla
            // 
            this.picPantalla.Location = new System.Drawing.Point(61, 41);
            this.picPantalla.Name = "picPantalla";
            this.picPantalla.Size = new System.Drawing.Size(497, 369);
            this.picPantalla.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPantalla.TabIndex = 4;
            this.picPantalla.TabStop = false;
            // 
            // timerPantalla
            // 
            this.timerPantalla.Tick += new System.EventHandler(this.timerPantalla_Tick);
            // 
            // FormPantalla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkControl);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.picPantalla);
            this.Name = "FormPantalla";
            this.Text = "FormPantalla";
            ((System.ComponentModel.ISupportInitialize)(this.picPantalla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkControl;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.PictureBox picPantalla;
        private System.Windows.Forms.Timer timerPantalla;
    }
}