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
    public partial class FrmLogin : Form
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        public FrmLogin()
        {
            InitializeComponent();

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                DataTable tabla = usuarioBLL.Login(
                    txtUsuario.Text,
                    txtClave.Text
                );

                if (tabla.Rows.Count > 0)
                {
                    // Login correcto
                    int idUsuario = Convert.ToInt32(tabla.Rows[0]["IdUsuario"]);
                    string nombre = tabla.Rows[0]["NombreCompleto"].ToString();

                    MessageBox.Show("Bienvenido " + nombre);

                    FrmPrincipal frm = new FrmPrincipal();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            _errorProvider.Clear();

            Control firstInvalid = null;

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                _errorProvider.SetError(txtUsuario, "Ingrese el usuario.");
                if (firstInvalid == null) firstInvalid = txtUsuario;
            }

            if (string.IsNullOrWhiteSpace(txtClave.Text))
            {
                _errorProvider.SetError(txtClave, "Ingrese la contraseña.");
                if (firstInvalid == null) firstInvalid = txtClave;
            }

            if (firstInvalid != null)
            {
                firstInvalid.Focus();
                return false;
            }

            return true;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnIngresar;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
