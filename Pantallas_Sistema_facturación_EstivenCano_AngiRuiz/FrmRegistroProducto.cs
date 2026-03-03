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

        ProductoBLL productoBLL = new ProductoBLL();
        CategoriaDAL categoriaDAL = new CategoriaDAL();
        public FrmRegistroProducto()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRegistroProducto_Load(object sender, EventArgs e)
        {
            cboCategoria.DataSource = categoriaDAL.MostrarCategorias();
            cboCategoria.DisplayMember = "Nombre";
            cboCategoria.ValueMember = "IdCategoria";

            txtPrecioCompra.KeyPress += SoloNumerosDecimales;
            txtPrecioVenta.KeyPress += SoloNumerosDecimales;
            txtStock.KeyPress += SoloNumerosEnteros;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                productoBLL.GuardarProducto(
                    EsEdicion,
                    IdProducto,
                    txtNombre.Text.Trim(),
                    txtCodigo.Text.Trim(),
                    Convert.ToDecimal(txtPrecioCompra.Text),
                    Convert.ToDecimal(txtPrecioVenta.Text),
                    Convert.ToInt32(txtStock.Text),
                    Convert.ToInt32(cboCategoria.SelectedValue),
                    txtRutaImagen.Text.Trim(),
                    txtDetalles.Text.Trim()
                );

                MessageBox.Show("Producto guardado correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SoloNumerosDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void SoloNumerosEnteros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
