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
    public partial class FrmCategoriaProductos : Form
    {
        public bool EsEdicion = false;
        public int IdCategoria = 0;

        CategoriaBLL categoriaBLL = new CategoriaBLL();
        public FrmCategoriaProductos()
        {
            InitializeComponent();
        }

        private void frmCategoriaProductos_Load(object sender, EventArgs e)
        {
            if (EsEdicion)
            {
                this.Text = "Actualizar Categoría";
                btnActualizar.Text = "Actualizar";
                CargarCategorias();
            }
            else
            {
                this.Text = "Nueva Categoría";
                btnActualizar.Text = "Guardar";
            }
        }

        private void CargarCategorias()
        {
            DataTable dt = categoriaBLL.MostrarCategorias();

            cboCategoria.DataSource = dt;
            cboCategoria.DisplayMember = "NombreCategoria";
            cboCategoria.ValueMember = "IdCategoria";

            // Activar autocompletado
            cboCategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCategoria.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
    }
}
