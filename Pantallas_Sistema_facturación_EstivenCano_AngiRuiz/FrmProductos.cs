using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
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

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            EstiloGrid();
            this.Dock = DockStyle.Fill;
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
            FrmRegistroProducto registroProducto = new FrmRegistroProducto();
            registroProducto.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmRegistroProducto registroProducto = new FrmRegistroProducto();
            registroProducto.ShowDialog();
        }
    }
}
