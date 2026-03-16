using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmFacturas : Form
    {
        private readonly ClienteBLL _clienteBll = new ClienteBLL();
        private readonly ProductoBLL _productoBll = new ProductoBLL();
        private readonly UsuarioBLL _usuarioBll = new UsuarioBLL();
        private readonly VentaBLL _ventaBll = new VentaBLL();

        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        private ComboBox cboCliente;
        private ComboBox cboEmpleado;
        private ComboBox cboProducto;
        private NumericUpDown nudCantidad;
        private Label lblPrecio;
        private Label lblStock;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Button btnAgregar;
        private Button btnQuitar;
        private Button btnGuardar;
        private Button btnLimpiar;

        private DataTable _productos;
        private DataTable _detalle;

        public FrmFacturas()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Facturas";
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

            var top = BuildHeader();
            var middle = BuildDetalle();
            var bottom = BuildFooter();

            root.Controls.Add(top, 0, 0);
            root.Controls.Add(middle, 0, 1);
            root.Controls.Add(bottom, 0, 2);

            Controls.Add(root);

            Load += FrmFacturas_Load;
        }

        private Control BuildHeader()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 4,
                RowCount = 2,
                AutoSize = true
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            var lblCliente = new Label { AutoSize = true, Text = "Cliente:" };
            cboCliente = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblEmpleado = new Label { AutoSize = true, Text = "Empleado:" };
            cboEmpleado = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };

            panel.Controls.Add(lblCliente, 0, 0);
            panel.Controls.Add(cboCliente, 1, 0);
            panel.Controls.Add(lblEmpleado, 2, 0);
            panel.Controls.Add(cboEmpleado, 3, 0);

            // Selector de producto
            var lblProducto = new Label { AutoSize = true, Text = "Producto:" };
            cboProducto = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cboProducto.SelectionChangeCommitted += (_, __) => UpdateProductoInfo();

            var lblCant = new Label { AutoSize = true, Text = "Cantidad:" };
            nudCantidad = new NumericUpDown { Minimum = 1, Maximum = 100000, Value = 1, Width = 120 };

            btnAgregar = new Button { Text = "Agregar", AutoSize = true };
            btnAgregar.Click += btnAgregar_Click;

            var rightInfo = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, FlowDirection = FlowDirection.LeftToRight };
            lblPrecio = new Label { AutoSize = true, Text = "Precio: -" };
            lblStock = new Label { AutoSize = true, Text = "Stock: -" };
            rightInfo.Controls.Add(lblPrecio);
            rightInfo.Controls.Add(new Label { AutoSize = true, Text = "  " });
            rightInfo.Controls.Add(lblStock);
            rightInfo.Controls.Add(new Label { AutoSize = true, Text = "  " });
            rightInfo.Controls.Add(btnAgregar);

            panel.Controls.Add(lblProducto, 0, 1);
            panel.Controls.Add(cboProducto, 1, 1);
            panel.Controls.Add(lblCant, 2, 1);
            panel.Controls.Add(rightInfo, 3, 1);

            return panel;
        }

        private Control BuildDetalle()
        {
            var panel = new Panel { Dock = DockStyle.Fill };

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

            panel.Controls.Add(dgvDetalle);
            return panel;
        }

        private Control BuildFooter()
        {
            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 8, 0, 0)
            };

            btnGuardar = new Button { Text = "Guardar Factura", AutoSize = true };
            btnGuardar.Click += btnGuardar_Click;

            btnQuitar = new Button { Text = "Quitar Item", AutoSize = true };
            btnQuitar.Click += btnQuitar_Click;

            btnLimpiar = new Button { Text = "Limpiar", AutoSize = true };
            btnLimpiar.Click += (_, __) => LimpiarFormulario();

            lblTotal = new Label { AutoSize = true, Text = "Total: $0.00", Font = new Font(Font, FontStyle.Bold) };

            panel.Controls.Add(btnGuardar);
            panel.Controls.Add(btnQuitar);
            panel.Controls.Add(btnLimpiar);
            panel.Controls.Add(new Label { AutoSize = true, Text = "   " });
            panel.Controls.Add(lblTotal);

            return panel;
        }

        private void FrmFacturas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDatos();
                CrearTablaDetalle();
                BindDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            // Clientes
            var clientes = _clienteBll.MostrarClientes();
            try
            {
                var dv = clientes.DefaultView;
                dv.RowFilter = "Estado = true";
                clientes = dv.ToTable();
            }
            catch
            {
                // Si el filtro falla por tipos/columnas, mostramos todos.
            }

            cboCliente.DataSource = clientes;
            cboCliente.DisplayMember = "NombreCompleto";
            cboCliente.ValueMember = "IdCliente";

            // Empleados
            var empleados = _usuarioBll.MostrarEmpleados();
            cboEmpleado.DataSource = empleados;
            cboEmpleado.DisplayMember = "NombreCompleto";
            cboEmpleado.ValueMember = "IdEmpleado";

            // Productos
            _productos = _productoBll.ObtenerProductos();
            try
            {
                var dvProd = _productos.DefaultView;
                dvProd.RowFilter = "Estado = true";
                _productos = dvProd.ToTable();
            }
            catch { }

            cboProducto.DataSource = _productos;
            cboProducto.DisplayMember = "Nombre";
            cboProducto.ValueMember = "IdProducto";

            UpdateProductoInfo();
        }

        private void CrearTablaDetalle()
        {
            _detalle = new DataTable();
            _detalle.Columns.Add("IdProducto", typeof(int));
            _detalle.Columns.Add("Producto", typeof(string));
            _detalle.Columns.Add("Cantidad", typeof(int));
            _detalle.Columns.Add("PrecioUnitario", typeof(decimal));
            _detalle.Columns.Add("SubTotal", typeof(decimal));
        }

        private void BindDetalle()
        {
            dgvDetalle.DataSource = _detalle;
            if (dgvDetalle.Columns["IdProducto"] != null)
                dgvDetalle.Columns["IdProducto"].Visible = false;
        }

        private void UpdateProductoInfo()
        {
            _errorProvider.SetError(cboProducto, string.Empty);

            if (_productos == null || cboProducto.SelectedValue == null)
            {
                lblPrecio.Text = "Precio: -";
                lblStock.Text = "Stock: -";
                return;
            }

            int idProducto = Convert.ToInt32(cboProducto.SelectedValue);
            DataRow row = FindProductoRow(idProducto);
            if (row == null)
            {
                lblPrecio.Text = "Precio: -";
                lblStock.Text = "Stock: -";
                return;
            }

            decimal precio = Convert.ToDecimal(row["PrecioVenta"]);
            int stock = Convert.ToInt32(row["Stock"]);
            lblPrecio.Text = "Precio: $" + precio.ToString("0.00");
            lblStock.Text = "Stock: " + stock;
        }

        private DataRow FindProductoRow(int idProducto)
        {
            if (_productos == null) return null;

            foreach (DataRow row in _productos.Rows)
            {
                if (Convert.ToInt32(row["IdProducto"]) == idProducto)
                    return row;
            }
            return null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();

                if (cboProducto.SelectedValue == null)
                {
                    _errorProvider.SetError(cboProducto, "Seleccione un producto.");
                    return;
                }

                int idProducto = Convert.ToInt32(cboProducto.SelectedValue);
                DataRow prod = FindProductoRow(idProducto);
                if (prod == null) throw new Exception("Producto no encontrado.");

                string nombre = Convert.ToString(prod["Nombre"]);
                decimal precio = Convert.ToDecimal(prod["PrecioVenta"]);
                int stock = Convert.ToInt32(prod["Stock"]);

                int cantidad = Convert.ToInt32(nudCantidad.Value);
                if (cantidad <= 0) throw new Exception("Cantidad invalida.");

                int cantidadActualEnDetalle = 0;
                DataRow existente = null;
                foreach (DataRow r in _detalle.Rows)
                {
                    if (Convert.ToInt32(r["IdProducto"]) == idProducto)
                    {
                        existente = r;
                        cantidadActualEnDetalle = Convert.ToInt32(r["Cantidad"]);
                        break;
                    }
                }

                if (cantidadActualEnDetalle + cantidad > stock)
                {
                    _errorProvider.SetError(nudCantidad, "La cantidad supera el stock disponible.");
                    return;
                }

                if (existente == null)
                {
                    decimal sub = cantidad * precio;
                    _detalle.Rows.Add(idProducto, nombre, cantidad, precio, sub);
                }
                else
                {
                    int nuevaCantidad = cantidadActualEnDetalle + cantidad;
                    existente["Cantidad"] = nuevaCantidad;
                    existente["PrecioUnitario"] = precio;
                    existente["SubTotal"] = nuevaCantidad * precio;
                }

                nudCantidad.Value = 1;
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null) return;

            var drv = dgvDetalle.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            drv.Row.Delete();
            _detalle.AcceptChanges();
            ActualizarTotal();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();

                if (cboCliente.SelectedValue == null)
                {
                    _errorProvider.SetError(cboCliente, "Seleccione un cliente.");
                    return;
                }

                if (cboEmpleado.SelectedValue == null)
                {
                    _errorProvider.SetError(cboEmpleado, "Seleccione un empleado.");
                    return;
                }

                if (_detalle.Rows.Count == 0)
                    throw new Exception("Agregue al menos un producto.");

                int idCliente = Convert.ToInt32(cboCliente.SelectedValue);
                int idEmpleado = Convert.ToInt32(cboEmpleado.SelectedValue);

                var items = new List<VentaDetalleItem>();
                foreach (DataRow r in _detalle.Rows)
                {
                    items.Add(new VentaDetalleItem
                    {
                        IdProducto = Convert.ToInt32(r["IdProducto"]),
                        Producto = Convert.ToString(r["Producto"]),
                        Cantidad = Convert.ToInt32(r["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(r["PrecioUnitario"])
                    });
                }

                int idVenta = _ventaBll.RegistrarVenta(idCliente, idEmpleado, items);

                MessageBox.Show("Factura registrada. Nro: " + idVenta, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarFormulario();
                CargarDatos(); // refresca stock
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            _errorProvider.Clear();

            if (_detalle != null)
            {
                _detalle.Rows.Clear();
                _detalle.AcceptChanges();
            }

            nudCantidad.Value = 1;
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total = 0m;
            if (_detalle != null)
            {
                foreach (DataRow r in _detalle.Rows)
                {
                    total += Convert.ToDecimal(r["SubTotal"]);
                }
            }

            lblTotal.Text = "Total: $" + total.ToString("0.00");
        }
    }
}
