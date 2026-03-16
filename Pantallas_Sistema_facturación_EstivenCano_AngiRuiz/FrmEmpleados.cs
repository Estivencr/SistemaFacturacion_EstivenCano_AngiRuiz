using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmEmpleados : Form
    {
        public bool EsEdicion { get; set; }
        public int IdEmpleado { get; set; }

        private readonly EmpleadoBLL _empleadoBll = new EmpleadoBLL();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        private TextBox txtNombre;
        private TextBox txtDocumento;
        private TextBox txtTelefono;
        private TextBox txtCargo;
        private Button btnGuardar;
        private Button btnCancelar;

        public FrmEmpleados()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Empleado";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Width = 520;
            Height = 320;

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(12)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            txtNombre = new TextBox { Dock = DockStyle.Fill };
            txtDocumento = new TextBox { Dock = DockStyle.Fill };
            txtTelefono = new TextBox { Dock = DockStyle.Fill };
            txtCargo = new TextBox { Dock = DockStyle.Fill };

            grid.Controls.Add(new Label { AutoSize = true, Text = "Nombre completo:" }, 0, 0);
            grid.Controls.Add(txtNombre, 1, 0);
            grid.Controls.Add(new Label { AutoSize = true, Text = "Documento:" }, 0, 1);
            grid.Controls.Add(txtDocumento, 1, 1);
            grid.Controls.Add(new Label { AutoSize = true, Text = "Telefono:" }, 0, 2);
            grid.Controls.Add(txtTelefono, 1, 2);
            grid.Controls.Add(new Label { AutoSize = true, Text = "Cargo:" }, 0, 3);
            grid.Controls.Add(txtCargo, 1, 3);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Padding = new Padding(0, 10, 0, 0)
            };

            btnGuardar = new Button { Text = "Guardar", AutoSize = true };
            btnGuardar.Click += btnGuardar_Click;

            btnCancelar = new Button { Text = "Cancelar", AutoSize = true };
            btnCancelar.Click += (_, __) => Close();

            actions.Controls.Add(btnGuardar);
            actions.Controls.Add(btnCancelar);

            Controls.Add(grid);
            Controls.Add(actions);

            AcceptButton = btnGuardar;

            Load += FrmEmpleados_Load;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            if (!EsEdicion) return;

            DataRow row = _empleadoBll.ObtenerEmpleadoPorId(IdEmpleado);
            if (row == null) return;

            txtNombre.Text = Convert.ToString(row["NombreCompleto"]);
            txtDocumento.Text = Convert.ToString(row["Documento"]);
            txtTelefono.Text = Convert.ToString(row["Telefono"]);
            txtCargo.Text = Convert.ToString(row["Cargo"]);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    _errorProvider.SetError(txtNombre, "Ingrese el nombre completo.");
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCargo.Text))
                {
                    _errorProvider.SetError(txtCargo, "Ingrese el cargo.");
                    txtCargo.Focus();
                    return;
                }

                _empleadoBll.GuardarEmpleado(
                    EsEdicion,
                    IdEmpleado,
                    txtNombre.Text,
                    txtDocumento.Text,
                    txtTelefono.Text,
                    txtCargo.Text);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
