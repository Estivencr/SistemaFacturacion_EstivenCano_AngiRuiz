namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlFondo    = new System.Windows.Forms.Panel();
            this.pnlCard     = new System.Windows.Forms.Panel();
            this.lblTitulo1  = new System.Windows.Forms.Label();
            this.lblTitulo2  = new System.Windows.Forms.Label();
            this.pnlLinea    = new System.Windows.Forms.Panel();
            this.label1      = new System.Windows.Forms.Label();
            this.txtUsuario  = new System.Windows.Forms.TextBox();
            this.label2      = new System.Windows.Forms.Label();
            this.txtClave    = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnSalir    = new System.Windows.Forms.Button();
            this.pnlFondo.SuspendLayout();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // ── pnlFondo ─────────────────────────────────────────
            this.pnlFondo.BackColor = System.Drawing.Color.FromArgb(26, 43, 74);
            this.pnlFondo.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.pnlFondo.Controls.Add(this.pnlCard);

            // ── pnlCard ───────────────────────────────────────────
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Size      = new System.Drawing.Size(370, 365);
            this.pnlCard.Location  = new System.Drawing.Point(16, 43);
            this.pnlCard.Controls.Add(this.lblTitulo1);
            this.pnlCard.Controls.Add(this.lblTitulo2);
            this.pnlCard.Controls.Add(this.pnlLinea);
            this.pnlCard.Controls.Add(this.label1);
            this.pnlCard.Controls.Add(this.txtUsuario);
            this.pnlCard.Controls.Add(this.label2);
            this.pnlCard.Controls.Add(this.txtClave);
            this.pnlCard.Controls.Add(this.btnIngresar);
            this.pnlCard.Controls.Add(this.btnSalir);

            // lblTitulo1
            this.lblTitulo1.Text      = "Sistema de";
            this.lblTitulo1.Font      = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo1.ForeColor = System.Drawing.Color.FromArgb(26, 43, 74);
            this.lblTitulo1.AutoSize  = true;
            this.lblTitulo1.Location  = new System.Drawing.Point(30, 22);

            // lblTitulo2
            this.lblTitulo2.Text      = "Facturación";
            this.lblTitulo2.Font      = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo2.ForeColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.lblTitulo2.AutoSize  = true;
            this.lblTitulo2.Location  = new System.Drawing.Point(30, 60);

            // pnlLinea
            this.pnlLinea.BackColor = System.Drawing.Color.FromArgb(220, 228, 235);
            this.pnlLinea.Location  = new System.Drawing.Point(30, 110);
            this.pnlLinea.Size      = new System.Drawing.Size(310, 1);

            // label1 — Usuario
            this.label1.Text      = "Usuario";
            this.label1.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.label1.AutoSize  = true;
            this.label1.Location  = new System.Drawing.Point(30, 124);

            // txtUsuario
            this.txtUsuario.Font        = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsuario.Location    = new System.Drawing.Point(30, 144);
            this.txtUsuario.Size        = new System.Drawing.Size(310, 28);
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.TabIndex    = 0;
            this.txtUsuario.Name        = "txtUsuario";

            // label2 — Contraseña
            this.label2.Text      = "Contrase\u00f1a";
            this.label2.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.label2.AutoSize  = true;
            this.label2.Location  = new System.Drawing.Point(30, 185);

            // txtClave
            this.txtClave.Font             = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClave.Location         = new System.Drawing.Point(30, 205);
            this.txtClave.Size             = new System.Drawing.Size(310, 28);
            this.txtClave.BorderStyle      = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClave.PasswordChar     = '*';
            this.txtClave.TabIndex         = 1;
            this.txtClave.Name             = "txtClave";

            // btnIngresar
            this.btnIngresar.Text      = "Ingresar";
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnIngresar.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.Location  = new System.Drawing.Point(30, 262);
            this.btnIngresar.Size      = new System.Drawing.Size(143, 42);
            this.btnIngresar.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.TabIndex  = 2;
            this.btnIngresar.Click    += new System.EventHandler(this.btnIngresar_Click);

            // btnSalir
            this.btnSalir.Text      = "Salir";
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(130, 140, 148);
            this.btnSalir.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalir.Location  = new System.Drawing.Point(197, 262);
            this.btnSalir.Size      = new System.Drawing.Size(143, 42);
            this.btnSalir.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.TabIndex  = 3;
            this.btnSalir.Click    += new System.EventHandler(this.btnSalir_Click);

            // ── FrmLogin ──────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(26, 43, 74);
            this.ClientSize          = new System.Drawing.Size(402, 450);
            this.Controls.Add(this.pnlFondo);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.None;
            this.Name                = "FrmLogin";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Sistema de Facturaci\u00f3n";
            this.Load               += new System.EventHandler(this.FrmLogin_Load);

            this.pnlFondo.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel   pnlFondo;
        private System.Windows.Forms.Panel   pnlCard;
        private System.Windows.Forms.Label   lblTitulo1;
        private System.Windows.Forms.Label   lblTitulo2;
        private System.Windows.Forms.Panel   pnlLinea;
        private System.Windows.Forms.Label   label1;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label   label2;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button  btnIngresar;
        private System.Windows.Forms.Button  btnSalir;
    }
}
