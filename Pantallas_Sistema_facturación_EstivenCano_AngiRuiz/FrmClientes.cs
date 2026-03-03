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
            if (dgvClientes.SelectedRows.Count > 0)
            {
                FrmRegistroClientes frm = new FrmRegistroClientes();

                // Pasamos el ID y los datos
                frm.IdClienteSeleccionado = Convert.ToInt32(dgvClientes.CurrentRow.Cells["IdCliente"].Value); // Verifica que el nombre de la columna sea "IdCliente" o "Id"

                frm.CargarDatos(
                    dgvClientes.CurrentRow.Cells["NombreCompleto"].Value.ToString(),
                    dgvClientes.CurrentRow.Cells["Documento"].Value.ToString(),
                    dgvClientes.CurrentRow.Cells["Direccion"].Value.ToString(),
                    dgvClientes.CurrentRow.Cells["Telefono"].Value.ToString(),
                    dgvClientes.CurrentRow.Cells["Email"].Value.ToString()
                );

                frm.ShowDialog();
                CargarClientes(); 
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa.");
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Si el usuario borra todo, cargamos todos los clientes
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarClientes();
            }
            else
            {
                // Usamos el método de búsqueda de la BLL
                dgvClientes.DataSource = clienteBLL.BuscarCliente(txtBuscar.Text);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                // 2. Pedir confirmación al usuario (Empatía con el usuario: ¡nadie quiere borrar por error!)
                DialogResult result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar al cliente seleccionado?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Obtener el ID del cliente seleccionado
                        // Asegúrate de que el nombre de la columna sea "IdCliente" como en tu DAL
                        int id = Convert.ToInt32(dgvClientes.CurrentRow.Cells["IdCliente"].Value);

                        // 4. Llamar a la Capa de Negocio para cambiar el estado a false (0)
                        clienteBLL.CambiarEstado(id, false);

                        // 5. Feedback y actualización
                        MessageBox.Show("El cliente ha sido eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarClientes(); // Refresca la tabla para que ya no aparezca
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al intentar eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa de la lista para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Length > 2) // Opcional: buscar solo si hay más de 2 letras
            {
                dgvClientes.DataSource = clienteBLL.BuscarCliente(txtBuscar.Text);
            }
            else if (txtBuscar.Text == "")
            {
                CargarClientes(); // Si limpia el buscador, recarga todos
            }
        }
    }
}
