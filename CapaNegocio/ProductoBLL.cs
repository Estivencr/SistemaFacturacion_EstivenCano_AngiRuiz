using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoBLL
    {
        ProductoDAL productoDAL = new ProductoDAL();

        public DataTable ObtenerProductos()
        {
            return productoDAL.ListarProductos();
        }

        public void GuardarProducto(
    bool esEdicion,
    int idProducto,
    string nombre,
    int codigo,
    decimal precioCompra,
    decimal precioVenta,
    int stock,
    int idCategoria,
    string rutaImagen,
    string detalles)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(codigo))
                throw new Exception("El código es obligatorio.");

            if (precioVenta <= precioCompra)
                throw new Exception("El precio de venta debe ser mayor al de compra.");

            if (stock < 0)
                throw new Exception("Stock inválido.");

            if (!esEdicion)
                productoDAL.InsertarProducto(
                    nombre, codigo, precioCompra, precioVenta,
                    stock, idCategoria, rutaImagen, detalles);
        }

        public void CambiarEstado(int idProducto, bool estado)
        {
            productoDAL.CambiarEstado(idProducto, estado);
        }
    }
}
