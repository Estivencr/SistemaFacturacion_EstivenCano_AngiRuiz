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
    public partial class FrmRegistroClientes : Form
    {
        public bool EsEdicion { get; set; } = false;
        public int IdClienteSeleccionado { get; set; }

        ClienteBLL clienteBLL = new ClienteBLL();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmRegistroClientes()
        {
            InitializeComponent();

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                // Llamamos al método de la Capa de Negocio que ya creaste
                clienteBLL.GuardarCliente(
                    EsEdicion,
                    IdClienteSeleccionado,
                    txtNombre.Text.Trim(),
                    txtDocumento.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtDireccion.Text.Trim()
                );

                MessageBox.Show("Operación realizada con éxito", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indica al padre que hubo cambios
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            _errorProvider.Clear();
            Control firstInvalid = null;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                _errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
                if (firstInvalid == null) firstInvalid = txtNombre;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                _errorProvider.SetError(txtDocumento, "El documento es obligatorio.");
                if (firstInvalid == null) firstInvalid = txtDocumento;
            }

            if (firstInvalid != null)
            {
                firstInvalid.Focus();
                return false;
            }

            return true;
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmRegistroClientes_Load(object sender, EventArgs e)
        {
      
        }

        public void CargarDatos(string nombre, string documento, string direccion, string telefono, string email)
        {
            txtNombre.Text = nombre;
            txtDocumento.Text = documento;
            txtDireccion.Text = direccion;
            txtTelefono.Text = telefono;
            txtEmail.Text = email;
            EsEdicion = true;
        }


    }
}
