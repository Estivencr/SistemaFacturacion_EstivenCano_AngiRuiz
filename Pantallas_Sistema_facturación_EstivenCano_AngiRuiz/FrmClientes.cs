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

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    public partial class FrmClientes : Form
    {
        ClienteBLL clienteBLL = new ClienteBLL();
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistroClientes frm = new FrmRegistroClientes();
            frm.ShowDialog();
            CargarClientes();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            dgvClientes.DataSource = clienteBLL.MostrarClientes();
        }

        private void EstiloGrid()
        {
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 123, 255);
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvClientes.BackgroundColor = Color.White;

            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 66, 91);
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form hijo in this.MdiChildren)
            {
                if (hijo is FrmClientes)
                {
                    hijo.Activate();
                    return;
                }
            }

            FrmClientes frm = new FrmClientes();
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null) return;

            FrmRegistroClientes frm = new FrmRegistroClientes();

            frm.IdCliente = Convert.ToInt32(
                dgvClientes.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
            CargarClientes();
        }


    }
}
