using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class ClienteBLL
    {
        private ClienteDAL clienteDAL;

        public ClienteBLL()
        {
            clienteDAL = new ClienteDAL();
        }

        public DataTable MostrarClientes()
        {
            return clienteDAL.MostrarClientes();
        }

        public DataTable BuscarCliente(string texto)
        {
            return clienteDAL.BuscarCliente(texto);
        }

        public void CambiarEstado(int idCliente, bool estado)
        {
            if (idCliente <= 0)
                throw new Exception("Cliente inválido.");

            clienteDAL.CambiarEstado(idCliente, estado);
        }

        public DataTable ObtenerClientePorId(int idCliente)
        {
            return clienteDAL.ObtenerClientePorId(idCliente);
        }

        public void GuardarCliente(bool esEdicion,
                           int idCliente,
                           string nombre,
                           string documento,
                           string telefono,
                           string email,
                           string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(documento))
                throw new Exception("El documento es obligatorio.");

            if (!esEdicion)
            {
                clienteDAL.InsertarCliente(nombre.Trim(),
                                            documento.Trim(),
                                            telefono.Trim(),
                                            email.Trim(),
                                            direccion.Trim());
            }
            else
            {
                clienteDAL.ActualizarCliente(idCliente,
                                             nombre.Trim(),
                                             documento.Trim(),
                                             telefono.Trim(),
                                             email.Trim(),
                                             direccion.Trim());
            }
        }
    }
}
