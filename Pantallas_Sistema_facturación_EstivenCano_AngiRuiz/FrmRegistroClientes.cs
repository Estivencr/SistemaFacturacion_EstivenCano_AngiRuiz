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
        public int IdCliente = 0;
        public bool EsEdicion = false;

        ClienteBLL clienteBLL = new ClienteBLL();

        public FrmRegistroClientes()
        {
            InitializeComponent();
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
                clienteBLL.GuardarCliente(
                    EsEdicion,
                    IdCliente,
                    txtNombre.Text,
                    txtDocumento.Text,
                    txtTelefono.Text,
                    txtEmail.Text,
                    txtDireccion.Text
                );

                MessageBox.Show("Cliente guardado correctamente.",
                                "Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmRegistroClientes_Load(object sender, EventArgs e)
        {
            if (EsEdicion)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            DataTable tabla = clienteBLL.ObtenerClientePorId(IdCliente);

            if (tabla.Rows.Count > 0)
            {
                txtNombre.Text = tabla.Rows[0]["NombreCompleto"].ToString();
                txtDocumento.Text = tabla.Rows[0]["Documento"].ToString();
                txtTelefono.Text = tabla.Rows[0]["Telefono"].ToString();
                txtEmail.Text = tabla.Rows[0]["Email"].ToString();
                txtDireccion.Text = tabla.Rows[0]["Direccion"].ToString();
            }
        }


    }
}
