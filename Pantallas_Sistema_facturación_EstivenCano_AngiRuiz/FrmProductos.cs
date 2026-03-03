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

        

        private void EstiloGrid()
        {
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductos.BackgroundColor = Color.White;

            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 66, 91);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistroProducto frm = new FrmRegistroProducto();
            frm.ShowDialog();
            CargarProductos();
        }
        

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0) return;

            FrmRegistroProducto frm = new FrmRegistroProducto();

            frm.EsEdicion = true;
            frm.IdProducto = Convert.ToInt32(
                dgvProductos.CurrentRow.Cells["IdProducto"].Value);

            frm.ShowDialog();
            CargarProductos();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

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
    }
}
