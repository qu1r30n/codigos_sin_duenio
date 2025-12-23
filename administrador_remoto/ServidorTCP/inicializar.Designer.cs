namespace ServidorTCP
{
    partial class inicializar
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
            this.lbl_puerto = new System.Windows.Forms.Label();
            this.txt_puerto = new System.Windows.Forms.TextBox();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_puerto
            // 
            this.lbl_puerto.AutoSize = true;
            this.lbl_puerto.Location = new System.Drawing.Point(12, 9);
            this.lbl_puerto.Name = "lbl_puerto";
            this.lbl_puerto.Size = new System.Drawing.Size(37, 13);
            this.lbl_puerto.TabIndex = 5;
            this.lbl_puerto.Text = "puerto";
            // 
            // txt_puerto
            // 
            this.txt_puerto.Location = new System.Drawing.Point(15, 35);
            this.txt_puerto.Name = "txt_puerto";
            this.txt_puerto.Size = new System.Drawing.Size(100, 20);
            this.txt_puerto.TabIndex = 4;
            this.txt_puerto.Text = "99";
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.Location = new System.Drawing.Point(29, 79);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(75, 23);
            this.btn_iniciar.TabIndex = 3;
            this.btn_iniciar.Text = "iniciar";
            this.btn_iniciar.UseVisualStyleBackColor = true;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // inicializar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(167, 125);
            this.Controls.Add(this.lbl_puerto);
            this.Controls.Add(this.txt_puerto);
            this.Controls.Add(this.btn_iniciar);
            this.Name = "inicializar";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_puerto;
        private System.Windows.Forms.TextBox txt_puerto;
        private System.Windows.Forms.Button btn_iniciar;
    }
}

