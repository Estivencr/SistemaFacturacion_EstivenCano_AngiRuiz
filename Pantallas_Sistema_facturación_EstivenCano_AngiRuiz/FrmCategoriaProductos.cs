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
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        public FrmCategoriaProductos()
        {
            InitializeComponent();

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void frmCategoriaProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            DataTable dt = categoriaBLL.MostrarCategorias();

            cboCategoria.DataSource = null;
            cboCategoria.DataSource = dt;
            cboCategoria.DisplayMember = "NombreCategoria";
            cboCategoria.ValueMember = "IdCategoria";
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombreCategoria.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _errorProvider.Clear();
                    _errorProvider.SetError(txtNombreCategoria, "Ingrese un nombre.");
                    txtNombreCategoria.Focus();
                    return;
                }

                _errorProvider.Clear();

                if (EsEdicion)
                {
                    categoriaBLL.ActualizarCategoria(IdCategoria, nombre);
                    MessageBox.Show("Categoría actualizada correctamente.");
                }
                else
                {
                    categoriaBLL.InsertarCategoria(nombre);
                    MessageBox.Show("Categoría registrada correctamente.");
                }

                CargarCategorias();

                txtNombreCategoria.Clear();
                cboCategoria.SelectedIndex = -1;
                EsEdicion = false;
                IdCategoria = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cboCategoria_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboCategoria.SelectedValue == null)
                return;

            _errorProvider.Clear();
            IdCategoria = Convert.ToInt32(cboCategoria.SelectedValue);
            txtNombreCategoria.Text = cboCategoria.Text;
            EsEdicion = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            txtNombreCategoria.Clear();
            cboCategoria.SelectedIndex = -1;

            IdCategoria = 0;
            EsEdicion = false;

            txtNombreCategoria.Focus();
        }
    }
}
