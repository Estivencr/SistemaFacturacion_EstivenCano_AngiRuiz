using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ProductoDAL
    {
        private Conexion conexion;

        public ProductoDAL()
        {
            conexion = new Conexion();
        }

        public DataTable ListarProductos()
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Productos", cn);

                DataTable tabla = new DataTable();
                da.Fill(tabla);
                return tabla;
            }
        }

        public void InsertarProducto(
    string nombre,
    string codigo,
    decimal precioCompra,
    decimal precioVenta,
    int stock,
    int idCategoria,
    string rutaImagen,
    string detalles)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Productos
        (Nombre, Codigo, PrecioCompra, PrecioVenta,
         Stock, IdCategoria, RutaImagen, Detalles)
         VALUES
        (@Nombre, @Codigo, @PrecioCompra, @PrecioVenta,
         @Stock, @IdCategoria, @RutaImagen, @Detalles)", cn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                cmd.Parameters.AddWithValue("@RutaImagen", rutaImagen);
                cmd.Parameters.AddWithValue("@Detalles", detalles);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarProducto(int idProducto,
                               string nombre,
                               string descripcion,
                               decimal precio,
                               int stock)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Productos SET
              Nombre=@Nombre,
              Descripcion=@Descripcion,
              Precio=@Precio,
              Stock=@Stock
              WHERE IdProducto=@IdProducto", cn);

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Stock", stock);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(int idProducto, bool estado)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Productos SET Estado=@Estado WHERE IdProducto=@Id",
                    cn);

                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@Id", idProducto);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
