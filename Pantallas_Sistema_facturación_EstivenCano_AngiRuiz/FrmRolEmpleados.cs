using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmRolEmpleados : Form
    {
        private readonly UsuarioBLL _usuarioBll = new UsuarioBLL();

        private DataGridView dgvUsuarios;
        private ComboBox cboRol;
        private Button btnAplicar;
        private Button btnRefrescar;
        private Label lblSeleccion;

        private DataTable _usuarios;
        private DataTable _roles;

        public FrmRolEmpleados()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Roles";
            WindowState = FormWindowState.Maximized;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(12)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            btnRefrescar = new Button { Text = "Refrescar", AutoSize = true };
            btnRefrescar.Click += (_, __) => CargarDatos();
            top.Controls.Add(btnRefrescar);

            dgvUsuarios = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvUsuarios.SelectionChanged += (_, __) => ActualizarSeleccion();

            var right = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };

            lblSeleccion = new Label { AutoSize = true, Text = "Seleccione un usuario..." };

            var lblRol = new Label { AutoSize = true, Text = "Rol:" };
            cboRol = new ComboBox
            {
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDown
            };

            btnAplicar = new Button { Text = "Aplicar Rol", AutoSize = true };
            btnAplicar.Click += (_, __) => AplicarRol();

            var stack = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            stack.Controls.Add(lblSeleccion);
            stack.Controls.Add(new Label { AutoSize = true, Text = " " });
            stack.Controls.Add(lblRol);
            stack.Controls.Add(cboRol);
            stack.Controls.Add(new Label { AutoSize = true, Text = " " });
            stack.Controls.Add(btnAplicar);

            right.Controls.Add(stack);

            root.Controls.Add(top, 0, 0);
            root.SetColumnSpan(top, 2);
            root.Controls.Add(dgvUsuarios, 0, 1);
            root.Controls.Add(right, 1, 1);

            Controls.Add(root);

            Load += (_, __) => CargarDatos();
        }

        private void CargarDatos()
        {
            _usuarios = _usuarioBll.ListarUsuarios();
            dgvUsuarios.DataSource = _usuarios;

            if (dgvUsuarios.Columns["IdUsuario"] != null) dgvUsuarios.Columns["IdUsuario"].HeaderText = "Id";
            if (dgvUsuarios.Columns["IdEmpleado"] != null) dgvUsuarios.Columns["IdEmpleado"].Visible = false;

            _roles = _usuarioBll.ListarRoles();
            cboRol.Items.Clear();
            cboRol.Items.Add("Administrador");
            cboRol.Items.Add("Vendedor");

            if (_roles != null)
            {
                foreach (DataRow r in _roles.Rows)
                {
                    string rol = Convert.ToString(r["Rol"]);
                    if (!string.IsNullOrWhiteSpace(rol) && !cboRol.Items.Contains(rol))
                        cboRol.Items.Add(rol);
                }
            }

            ActualizarSeleccion();
        }

        private DataRowView GetSelectedUser()
        {
            if (dgvUsuarios.CurrentRow == null) return null;
            return dgvUsuarios.CurrentRow.DataBoundItem as DataRowView;
        }

        private void ActualizarSeleccion()
        {
            var drv = GetSelectedUser();
            if (drv == null)
            {
                lblSeleccion.Text = "Seleccione un usuario...";
                cboRol.Text = string.Empty;
                return;
            }

            string usuario = Convert.ToString(drv.Row["Usuario"]);
            string empleado = Convert.ToString(drv.Row["NombreCompleto"]);
            string rolActual = Convert.ToString(drv.Row["Rol"]);

            lblSeleccion.Text = "Usuario: " + usuario + " (" + empleado + ")";
            cboRol.Text = rolActual;
        }

        private void AplicarRol()
        {
            try
            {
                var drv = GetSelectedUser();
                if (drv == null) return;

                int idUsuario = Convert.ToInt32(drv.Row["IdUsuario"]);
                string rol = cboRol.Text;

                _usuarioBll.ActualizarRol(idUsuario, rol);

                int selectedIndex = dgvUsuarios.CurrentRow != null ? dgvUsuarios.CurrentRow.Index : -1;
                CargarDatos();
                if (selectedIndex >= 0 && selectedIndex < dgvUsuarios.Rows.Count)
                    dgvUsuarios.CurrentCell = dgvUsuarios.Rows[selectedIndex].Cells[0];

                MessageBox.Show("Rol actualizado.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
