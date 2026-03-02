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
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
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
    }
}
