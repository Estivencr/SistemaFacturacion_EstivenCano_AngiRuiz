using CapaDatos;
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
    public partial class FrmAdminSeguridad : Form
    {
        public bool EsEdicion = false;
        public int IdUsuario = 0;
        public int IdEmpleadoSeleccionado { get; set; }
        public string UsuarioActual { get; set; }

        UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly ErrorProvider _errorProvider = new ErrorProvider();

        public FrmAdminSeguridad()
        {
            InitializeComponent();

            _errorProvider.ContainerControl = this;
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAdminSeguridad_Load(object sender, EventArgs e)
        {
            cboEmpleado.DataSource = usuarioBLL.MostrarEmpleados();
            cboEmpleado.DisplayMember = "NombreCompleto";
            cboEmpleado.ValueMember = "IdEmpleado";

            if (EsEdicion)
            {
                btnActualizar.Text = "Actualizar";
                txtUsuario.Enabled = false;

                if (IdEmpleadoSeleccionado > 0)
                    cboEmpleado.SelectedValue = IdEmpleadoSeleccionado;

                if (!string.IsNullOrWhiteSpace(UsuarioActual))
                    txtUsuario.Text = UsuarioActual;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                _errorProvider.Clear();

                if (cboEmpleado.SelectedValue == null)
                {
                    _errorProvider.SetError(cboEmpleado, "Seleccione un empleado.");
                    return;
                }

                if (!EsEdicion && string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    _errorProvider.SetError(txtUsuario, "Ingrese el usuario.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClave.Text))
                {
                    _errorProvider.SetError(txtClave, "Ingrese la clave.");
                    return;
                }

                if (!EsEdicion)
                {
                    usuarioBLL.InsertarUsuario(
                        Convert.ToInt32(cboEmpleado.SelectedValue),
                        txtUsuario.Text,
                        txtClave.Text
                    );

                    MessageBox.Show("Usuario creado correctamente.");
                }
                else
                {
                    usuarioBLL.ActualizarUsuario(
                        IdUsuario,
                        Convert.ToInt32(cboEmpleado.SelectedValue),
                        txtClave.Text
                    );

                    MessageBox.Show("Usuario actualizado.");
                }

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
