using System.Drawing;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmEmpleados : Form
    {
        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var lbl = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Text = "frmEmpleados (pendiente de implementación)"
            };

            AutoScaleMode = AutoScaleMode.Font;
            Text = "Empleados";
            Controls.Add(lbl);
        }
    }
}
