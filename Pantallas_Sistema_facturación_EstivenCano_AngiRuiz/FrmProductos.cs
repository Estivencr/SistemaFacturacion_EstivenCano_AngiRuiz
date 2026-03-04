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
    public partial class FrmProductos : Form
    {
        ProductoBLL productoBLL = new ProductoBLL();
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlBotones_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistroProducto frm = new FrmRegistroProducto();
            frm.ShowDialog();
            CargarProductos();
        }
        

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para editar.");
                return;
            }

            int idProducto = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["IdProducto"].Value);

            FrmRegistroProducto frm = new FrmRegistroProducto();
            frm.EsEdicion = true;
            frm.IdProducto = idProducto;

            frm.ShowDialog();

            CargarProductos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dgvProductos.DataSource = productoBLL.BuscarProductos(txtBuscar.Text.Trim());
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CargarProductos()
        {
            dgvProductos.DataSource = productoBLL.ObtenerProductos();

            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            int id = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value);

            var confirm = MessageBox.Show("¿Desea eliminar este producto?",
                                          "Confirmar",
                                          MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                productoBLL.EliminarProducto(id);
                CargarProductos();
            }
        }
    }
}
