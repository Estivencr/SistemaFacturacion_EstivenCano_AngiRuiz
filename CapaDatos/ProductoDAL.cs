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
        (Nombre, CodigoReferencia, PrecioCompra, PrecioVenta,
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

        public void ActualizarProducto(
    int idProducto,
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
                @"UPDATE Productos SET
            Nombre=@Nombre,
            CodigoReferencia=@Codigo,
            PrecioCompra=@PrecioCompra,
            PrecioVenta=@PrecioVenta,
            Stock=@Stock,
            IdCategoria=@IdCategoria,
            RutaImagen=@RutaImagen,
            Detalles=@Detalles
          WHERE IdProducto=@IdProducto", cn);

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
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

        public DataRow ObtenerProductoPorId(int idProducto)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT IdProducto,
                     Nombre,
                     CodigoReferencia,
                     PrecioCompra,
                     PrecioVenta,
                     Stock,
                     IdCategoria,
                     RutaImagen,
                     Detalles
              FROM Productos
              WHERE IdProducto = @IdProducto",
                    cn);

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            bool eliminado = false;

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Productos WHERE IdProducto = @IdProducto",
                    cn);

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                cn.Open();
                int filas = cmd.ExecuteNonQuery();

                eliminado = filas > 0;
            }

            return eliminado;
        }

        public DataTable BuscarProductos(string nombre)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Productos WHERE Nombre LIKE @Nombre",
                    cn);

                cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public bool ExisteCodigo(string codigo)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Productos WHERE CodigoReferencia = @Codigo",
                    cn);

                cmd.Parameters.AddWithValue("@Codigo", codigo);

                cn.Open();
                int cantidad = (int)cmd.ExecuteScalar();

                return cantidad > 0;
            }
        }

        public bool ExisteCodigoEnOtroProducto(string codigo, int idProducto)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(*) 
              FROM Productos 
              WHERE CodigoReferencia = @Codigo 
              AND IdProducto <> @IdProducto",
                    cn);

                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                cn.Open();
                int cantidad = (int)cmd.ExecuteScalar();

                return cantidad > 0;
            }
        }
    }
}
