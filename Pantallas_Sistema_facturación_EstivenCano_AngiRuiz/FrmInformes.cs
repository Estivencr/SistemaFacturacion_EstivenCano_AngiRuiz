using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmInformes : Form
    {
        private readonly ClienteBLL _clienteBll = new ClienteBLL();
        private readonly UsuarioBLL _usuarioBll = new UsuarioBLL();
        private readonly InformeBLL _informeBll = new InformeBLL();

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        private CheckBox chkDesde;
        private DateTimePicker dtpDesde;
        private CheckBox chkHasta;
        private DateTimePicker dtpHasta;
        private ComboBox cboCliente;
        private ComboBox cboEmpleado;
        private Button btnBuscar;

        private DataGridView dgvVentas;
        private DataGridView dgvDetalle;
        private Label lblTotalVentas;

        private DataTable _ventas;

        public FrmInformes()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Informes";
            WindowState = FormWindowState.Maximized;

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(BuildFiltros(), 0, 0);
            root.Controls.Add(BuildGrids(), 0, 1);
            root.Controls.Add(BuildFooter(), 0, 2);

            Controls.Add(root);

            Load += FrmInformes_Load;
        }

        private Control BuildFiltros()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 8,
                RowCount = 2,
                AutoSize = true
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            chkDesde = new CheckBox { AutoSize = true, Text = "Desde:" };
            dtpDesde = new DateTimePicker { Width = 160, Format = DateTimePickerFormat.Short, Enabled = false };
            chkDesde.CheckedChanged += (_, __) => dtpDesde.Enabled = chkDesde.Checked;

            chkHasta = new CheckBox { AutoSize = true, Text = "Hasta:" };
            dtpHasta = new DateTimePicker { Width = 160, Format = DateTimePickerFormat.Short, Enabled = false };
            chkHasta.CheckedChanged += (_, __) => dtpHasta.Enabled = chkHasta.Checked;

            var lblCliente = new Label { AutoSize = true, Text = "Cliente:" };
            cboCliente = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblEmpleado = new Label { AutoSize = true, Text = "Empleado:" };
            cboEmpleado = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            btnBuscar = new Button { Text = "Buscar", AutoSize = true };
            btnBuscar.Click += (_, __) => Buscar();

            panel.Controls.Add(chkDesde, 0, 0);
            panel.Controls.Add(dtpDesde, 1, 0);
            panel.Controls.Add(chkHasta, 2, 0);
            panel.Controls.Add(dtpHasta, 3, 0);

            panel.Controls.Add(lblCliente, 0, 1);
            panel.Controls.Add(cboCliente, 1, 1);
            panel.SetColumnSpan(cboCliente, 4);

            panel.Controls.Add(lblEmpleado, 5, 1);
            panel.Controls.Add(cboEmpleado, 6, 1);
            panel.Controls.Add(btnBuscar, 7, 1);

            return panel;
        }

        private Control BuildGrids()
        {
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 260
            };

            dgvVentas = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvVentas.SelectionChanged += dgvVentas_SelectionChanged;

            dgvDetalle = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            split.Panel1.Controls.Add(dgvVentas);
            split.Panel2.Controls.Add(dgvDetalle);

            return split;
        }

        private Control BuildFooter()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 8, 0, 0)
            };

            lblTotalVentas = new Label { AutoSize = true, Font = new Font(Font, FontStyle.Bold), Text = "Total vendido: $0.00" };
            panel.Controls.Add(lblTotalVentas);
            return panel;
        }

        private void FrmInformes_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombos();
                Buscar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCombos()
        {
            // Clientes: agregamos opcion "Todos"
            var clientes = _clienteBll.MostrarClientes();
            DataRow allCliente = clientes.NewRow();
            allCliente["IdCliente"] = 0;
            allCliente["NombreCompleto"] = "(Todos)";
            clientes.Rows.InsertAt(allCliente, 0);

            cboCliente.DataSource = clientes;
            cboCliente.DisplayMember = "NombreCompleto";
            cboCliente.ValueMember = "IdCliente";

            // Empleados: opcion "Todos"
            var empleados = _usuarioBll.MostrarEmpleados();
            DataRow allEmp = empleados.NewRow();
            allEmp["IdEmpleado"] = 0;
            allEmp["NombreCompleto"] = "(Todos)";
            empleados.Rows.InsertAt(allEmp, 0);

            cboEmpleado.DataSource = empleados;
            cboEmpleado.DisplayMember = "NombreCompleto";
            cboEmpleado.ValueMember = "IdEmpleado";
        }

        private void Buscar()
        {
            try
            {
                _errorProvider.Clear();

                DateTime? desde = chkDesde.Checked ? (DateTime?)dtpDesde.Value.Date : null;
                DateTime? hasta = chkHasta.Checked ? (DateTime?)dtpHasta.Value.Date : null;

                int idCliente = cboCliente.SelectedValue == null ? 0 : Convert.ToInt32(cboCliente.SelectedValue);
                int idEmpleado = cboEmpleado.SelectedValue == null ? 0 : Convert.ToInt32(cboEmpleado.SelectedValue);

                _ventas = _informeBll.ObtenerVentas(desde, hasta, idCliente, idEmpleado);
                dgvVentas.DataSource = _ventas;

                if (dgvVentas.Columns["IdVenta"] != null)
                    dgvVentas.Columns["IdVenta"].HeaderText = "Nro";

                ActualizarTotal();
                CargarDetalleVentaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVentas_SelectionChanged(object sender, EventArgs e)
        {
            CargarDetalleVentaSeleccionada();
        }

        private void CargarDetalleVentaSeleccionada()
        {
            try
            {
                if (dgvVentas.CurrentRow == null)
                {
                    dgvDetalle.DataSource = null;
                    return;
                }

                var drv = dgvVentas.CurrentRow.DataBoundItem as DataRowView;
                if (drv == null)
                {
                    dgvDetalle.DataSource = null;
                    return;
                }

                int idVenta = Convert.ToInt32(drv.Row["IdVenta"]);
                var dt = _informeBll.ObtenerDetalleVenta(idVenta);
                dgvDetalle.DataSource = dt;

                if (dgvDetalle.Columns["IdDetalle"] != null)
                    dgvDetalle.Columns["IdDetalle"].Visible = false;
                if (dgvDetalle.Columns["IdProducto"] != null)
                    dgvDetalle.Columns["IdProducto"].Visible = false;
            }
            catch
            {
                // No bloquea la pantalla si falla el detalle.
                dgvDetalle.DataSource = null;
            }
        }

        private void ActualizarTotal()
        {
            decimal total = 0m;

            if (_ventas != null)
            {
                foreach (DataRow r in _ventas.Rows)
                {
                    try { total += Convert.ToDecimal(r["Total"]); }
                    catch { }
                }
            }

            lblTotalVentas.Text = "Total vendido: $" + total.ToString("0.00");
        }
    }
}
