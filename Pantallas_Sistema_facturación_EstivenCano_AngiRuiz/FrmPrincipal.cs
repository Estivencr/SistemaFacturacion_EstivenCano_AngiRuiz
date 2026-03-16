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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();

            // En el Designer hay items de menu, pero algunos no estaban conectados.
            facturasToolStripMenuItem.Click += facturasToolStripMenuItem_Click;
            informesToolStripMenuItem.Click += informesToolStripMenuItem_Click;
            rolesToolStripMenuItem.Click += rolesToolStripMenuItem_Click;
            empleadosToolStripMenuItem.Click += empleadosToolStripMenuItem_Click;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAyuda ayuda = new FrmAyuda();
            ayuda.MdiParent = this;
            ayuda.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAcercaDe acercaDe = new FrmAcercaDe();
            acercaDe.MdiParent = this;
            acercaDe.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            clientes.MdiParent = this;
            clientes.Dock = DockStyle.Fill;
            clientes.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductos productos = new FrmProductos();
            productos.MdiParent = this;
            productos.Dock = DockStyle.Fill;
            productos.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoriaProductos categorias = new FrmCategoriaProductos();
            categorias.MdiParent = this;
            categorias.Show();
        }

        private void seguridadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirMdi(new FrmSeguridad());
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirMdi(new FrmFacturas());
        }

        private void informesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirMdi(new FrmInformes());
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirMdi(new FrmRolEmpleados());
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirMdi(new FrmListaEmpleados());
        }

        private void AbrirMdi(Form form)
        {
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
    }
}
