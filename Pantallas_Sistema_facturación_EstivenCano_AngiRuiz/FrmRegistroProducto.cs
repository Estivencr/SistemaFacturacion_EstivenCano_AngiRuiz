using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmRegistroProducto : Form
    {
        public bool EsEdicion = false;
        public int IdProducto = 0;

        private ProductoBLL productoBLL = new ProductoBLL();
        private CategoriaBLL categoriaBLL = new CategoriaBLL();

        public FrmRegistroProducto()
        {
            InitializeComponent();
            this.AcceptButton = btnGuardar;
        }

        private void FrmRegistroProducto_Load(object sender, EventArgs e)
        {
            CargarCategorias();

            if (EsEdicion)
            {
                this.Text = "Editar Producto";
                CargarProducto();
            }
            else
            {
                this.Text = "Registrar Producto";
            }

            txtPrecioCompra.KeyPress += SoloNumerosDecimales;
            txtPrecioVenta.KeyPress += SoloNumerosDecimales;
            txtStock.KeyPress += SoloNumerosEnteros;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                decimal precioCompra;
                decimal precioVenta;
                int stock;

                if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra) ||
                    !decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
                {
                    MessageBox.Show("Formato de precio inválido.");
                    return;
                }

                if (!int.TryParse(txtStock.Text, out stock))
                {
                    MessageBox.Show("Formato de stock inválido.");
                    return;
                }

                if (precioCompra <= 0 || precioVenta <= 0)
                {
                    MessageBox.Show("Los precios deben ser mayores a 0.");
                    return;
                }

                if (precioVenta < precioCompra)
                {
                    MessageBox.Show("El precio de venta no puede ser menor al de compra.");
                    return;
                }

                if (stock < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.");
                    return;
                }

                int idCategoria = Convert.ToInt32(cboCategoria.SelectedValue);

                productoBLL.GuardarProducto(
                    EsEdicion,
                    IdProducto,
                    txtNombre.Text.Trim(),
                    txtCodigo.Text.Trim(),
                    precioCompra,
                    precioVenta,
                    stock,
                    idCategoria,
                    txtRutaImagen.Text.Trim(),
                    txtDetalles.Text.Trim()
                );

                MessageBox.Show("Producto guardado correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese el código del producto.");
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioCompra.Text))
            {
                MessageBox.Show("Ingrese el precio de compra.");
                txtPrecioCompra.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Ingrese el precio de venta.");
                txtPrecioVenta.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Ingrese el stock.");
                txtStock.Focus();
                return false;
            }

            if (cboCategoria.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una categoría.");
                cboCategoria.Focus();
                return false;
            }

            return true;
        }

        private void CargarCategorias()
        {
            cboCategoria.DataSource = null;
            cboCategoria.DataSource = categoriaBLL.MostrarCategorias();
            cboCategoria.DisplayMember = "NombreCategoria";
            cboCategoria.ValueMember = "IdCategoria";
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarProducto()
        {
            DataRow row = productoBLL.ObtenerProductoPorId(IdProducto);

            if (row != null)
            {
                txtNombre.Text = row["Nombre"].ToString();
                txtCodigo.Text = row["CodigoReferencia"].ToString();
                txtPrecioCompra.Text = row["PrecioCompra"].ToString();
                txtPrecioVenta.Text = row["PrecioVenta"].ToString();
                txtStock.Text = row["Stock"].ToString();
                cboCategoria.SelectedValue = Convert.ToInt32(row["IdCategoria"]);
                txtRutaImagen.Text = row["RutaImagen"].ToString();
                txtDetalles.Text = row["Detalles"].ToString();
            }
        }

        private void SoloNumerosDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SoloNumerosEnteros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCodigo.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            txtStock.Clear();
            txtRutaImagen.Clear();
            txtDetalles.Clear();
            cboCategoria.SelectedIndex = -1;
        }
    }
}