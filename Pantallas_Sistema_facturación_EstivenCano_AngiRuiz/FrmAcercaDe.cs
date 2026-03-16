using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmAcercaDe : Form
    {
        public FrmAcercaDe()
        {
            InitializeComponent();

            Text = "Acerca de";
            StartPosition = FormStartPosition.CenterParent;

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            var lblTitulo = new Label
            {
                AutoSize = true,
                Font = new Font(Font, FontStyle.Bold),
                Text = "Pantallas_Sistema_facturación"
            };

            var txtInfo = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Width = 520,
                Height = 180,
                Text =
                    "Práctica: Herramientas de Programación III\r\n" +
                    "Laboratorio Nro. 1\r\n\r\n" +
                    "Este aplicativo es una base de pantallas para futuras prácticas.\r\n" +
                    "Incluye login, menú principal y CRUD básicos."
            };

            var btnCerrar = new Button
            {
                Text = "Cerrar",
                AutoSize = true
            };
            btnCerrar.Click += (_, __) => Close();

            lblTitulo.Location = new Point(20, 20);
            txtInfo.Location = new Point(20, 60);
            btnCerrar.Location = new Point(20, 260);

            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(txtInfo);
            panel.Controls.Add(btnCerrar);
            Controls.Add(panel);
        }
    }
}
