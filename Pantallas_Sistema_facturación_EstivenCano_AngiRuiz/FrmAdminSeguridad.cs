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

        UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FrmAdminSeguridad()
        {
            InitializeComponent();
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
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
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

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
