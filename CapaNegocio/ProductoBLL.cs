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
        int IdProducto,
        string Nombre,
        string CodigoReferencia,
        decimal PrecioCompra,
        decimal PrecioVenta,
        int Stock,
        int IdCategoria,
        string RutaImagen,
        string Detalles)
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                    throw new Exception("El nombre es obligatorio.");

                if (string.IsNullOrWhiteSpace(CodigoReferencia))
                    throw new Exception("El código es obligatorio.");

                if (PrecioVenta <= PrecioCompra)
                    throw new Exception("El precio de venta debe ser mayor al de compra.");

                if (Stock < 0)
                    throw new Exception("Stock inválido.");

                if (esEdicion)
                {
                    productoDAL.ActualizarProducto(
                        IdProducto,
                        Nombre,
                        CodigoReferencia,
                        PrecioCompra,
                        PrecioVenta,
                        Stock,
                        IdCategoria,
                        RutaImagen,
                        Detalles);
                }
                else
                {
                    productoDAL.InsertarProducto(
                        Nombre,
                        CodigoReferencia,
                        PrecioCompra,
                        PrecioVenta,
                        Stock,
                        IdCategoria,
                        RutaImagen,
                        Detalles);
                }
        }

        public void CambiarEstado(int idProducto, bool estado)
        {
            productoDAL.CambiarEstado(idProducto, estado);
        }

        public DataRow ObtenerProductoPorId(int id)
        {
            return productoDAL.ObtenerProductoPorId(id);
        }

        public bool EliminarProducto(int idProducto)
        {
            return productoDAL.EliminarProducto(idProducto);
        }

        public DataTable BuscarProductos(string texto)
        {
            return productoDAL.BuscarProductos(texto);
        }

        public bool ExisteCodigo(string codigo)
        {
            return productoDAL.ExisteCodigo(codigo);
        }

        public bool ExisteCodigoEnOtroProducto(string codigo, int idProducto)
        {
            return productoDAL.ExisteCodigoEnOtroProducto(codigo, idProducto);
        }
    }
}
