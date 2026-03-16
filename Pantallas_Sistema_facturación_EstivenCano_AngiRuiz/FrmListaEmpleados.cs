using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmListaEmpleados : Form
    {
        private readonly EmpleadoBLL _empleadoBll = new EmpleadoBLL();

        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnNuevo;
        private Button btnEditar;
        private Button btnActivarDesactivar;
        private Button btnRefrescar;
        private DataGridView dgvEmpleados;

        private DataTable _empleados;

        public FrmListaEmpleados()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Lista de Empleados";
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

            var top = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            txtBuscar = new TextBox { Width = 280 };
            btnBuscar = new Button { Text = "Buscar", AutoSize = true };
            btnBuscar.Click += (_, __) => Buscar();

            btnNuevo = new Button { Text = "Nuevo", AutoSize = true };
            btnNuevo.Click += (_, __) => Nuevo();

            btnEditar = new Button { Text = "Editar", AutoSize = true };
            btnEditar.Click += (_, __) => Editar();

            btnActivarDesactivar = new Button { Text = "Activar/Desactivar", AutoSize = true };
            btnActivarDesactivar.Click += (_, __) => ToggleEstado();

            btnRefrescar = new Button { Text = "Refrescar", AutoSize = true };
            btnRefrescar.Click += (_, __) => Cargar();

            top.Controls.Add(new Label { AutoSize = true, Text = "Buscar:" });
            top.Controls.Add(txtBuscar);
            top.Controls.Add(btnBuscar);
            top.Controls.Add(btnNuevo);
            top.Controls.Add(btnEditar);
            top.Controls.Add(btnActivarDesactivar);
            top.Controls.Add(btnRefrescar);

            dgvEmpleados = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            root.Controls.Add(top, 0, 0);
            root.Controls.Add(dgvEmpleados, 0, 1);

            Controls.Add(root);

            Load += (_, __) => Cargar();
        }

        private void Cargar()
        {
            _empleados = _empleadoBll.ListarEmpleados();
            dgvEmpleados.DataSource = _empleados;

            if (dgvEmpleados.Columns["IdEmpleado"] != null) dgvEmpleados.Columns["IdEmpleado"].HeaderText = "Id";
        }

        private void Buscar()
        {
            string texto = txtBuscar.Text.Trim();
            _empleados = string.IsNullOrWhiteSpace(texto)
                ? _empleadoBll.ListarEmpleados()
                : _empleadoBll.BuscarEmpleados(texto);

            dgvEmpleados.DataSource = _empleados;
        }

        private DataRowView GetSelected()
        {
            if (dgvEmpleados.CurrentRow == null) return null;
            return dgvEmpleados.CurrentRow.DataBoundItem as DataRowView;
        }

        private void Nuevo()
        {
            using (var frm = new FrmEmpleados())
            {
                frm.EsEdicion = false;
                if (frm.ShowDialog(this) == DialogResult.OK)
                    Cargar();
            }
        }

        private void Editar()
        {
            var drv = GetSelected();
            if (drv == null) return;

            int id = Convert.ToInt32(drv.Row["IdEmpleado"]);

            using (var frm = new FrmEmpleados())
            {
                frm.EsEdicion = true;
                frm.IdEmpleado = id;
                if (frm.ShowDialog(this) == DialogResult.OK)
                    Cargar();
            }
        }

        private void ToggleEstado()
        {
            var drv = GetSelected();
            if (drv == null) return;

            int id = Convert.ToInt32(drv.Row["IdEmpleado"]);
            bool estado = Convert.ToBoolean(drv.Row["Estado"]);

            _empleadoBll.CambiarEstado(id, !estado);
            Cargar();
        }
    }
}

