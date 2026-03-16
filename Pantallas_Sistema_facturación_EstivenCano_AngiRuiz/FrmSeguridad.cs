using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmSeguridad : Form
    {
        private readonly UsuarioBLL _usuarioBll = new UsuarioBLL();

        private DataGridView dgvUsuarios;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnActivarDesactivar;
        private Button btnRefrescar;

        private DataTable _usuarios;

        public FrmSeguridad()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Seguridad";
            WindowState = FormWindowState.Maximized;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(12)
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            btnNuevo = new Button { Text = "Nuevo", AutoSize = true };
            btnNuevo.Click += (_, __) => Nuevo();

            btnEditar = new Button { Text = "Editar", AutoSize = true };
            btnEditar.Click += (_, __) => Editar();

            btnActivarDesactivar = new Button { Text = "Activar/Desactivar", AutoSize = true };
            btnActivarDesactivar.Click += (_, __) => ToggleEstado();

            btnRefrescar = new Button { Text = "Refrescar", AutoSize = true };
            btnRefrescar.Click += (_, __) => CargarUsuarios();

            toolbar.Controls.Add(btnNuevo);
            toolbar.Controls.Add(btnEditar);
            toolbar.Controls.Add(btnActivarDesactivar);
            toolbar.Controls.Add(btnRefrescar);

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

            root.Controls.Add(toolbar, 0, 0);
            root.Controls.Add(dgvUsuarios, 0, 1);

            Controls.Add(root);

            Load += (_, __) => CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            _usuarios = _usuarioBll.ListarUsuarios();
            dgvUsuarios.DataSource = _usuarios;

            if (dgvUsuarios.Columns["IdUsuario"] != null) dgvUsuarios.Columns["IdUsuario"].HeaderText = "Id";
            if (dgvUsuarios.Columns["IdEmpleado"] != null) dgvUsuarios.Columns["IdEmpleado"].Visible = false;
            if (dgvUsuarios.Columns["NombreCompleto"] != null) dgvUsuarios.Columns["NombreCompleto"].HeaderText = "Empleado";
        }

        private DataRowView GetSelectedRow()
        {
            if (dgvUsuarios.CurrentRow == null) return null;
            return dgvUsuarios.CurrentRow.DataBoundItem as DataRowView;
        }

        private void Nuevo()
        {
            using (var frm = new FrmAdminSeguridad())
            {
                var res = frm.ShowDialog(this);
                if (res == DialogResult.OK)
                    CargarUsuarios();
            }
        }

        private void Editar()
        {
            var drv = GetSelectedRow();
            if (drv == null) return;

            int idUsuario = Convert.ToInt32(drv.Row["IdUsuario"]);
            int idEmpleado = Convert.ToInt32(drv.Row["IdEmpleado"]);
            string usuario = Convert.ToString(drv.Row["Usuario"]);

            using (var frm = new FrmAdminSeguridad())
            {
                frm.EsEdicion = true;
                frm.IdUsuario = idUsuario;
                frm.IdEmpleadoSeleccionado = idEmpleado;
                frm.UsuarioActual = usuario;

                var res = frm.ShowDialog(this);
                if (res == DialogResult.OK)
                    CargarUsuarios();
            }
        }

        private void ToggleEstado()
        {
            var drv = GetSelectedRow();
            if (drv == null) return;

            int idUsuario = Convert.ToInt32(drv.Row["IdUsuario"]);
            bool estado = Convert.ToBoolean(drv.Row["Estado"]);

            _usuarioBll.CambiarEstado(idUsuario, !estado);
            CargarUsuarios();
        }
    }
}

