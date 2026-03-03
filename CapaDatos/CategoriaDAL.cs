using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CategoriaDAL
    {
        Conexion conexion;

        public CategoriaDAL()
        {
            conexion = new Conexion();
        }

        // Mostrar solo activas (para ComboBox)
        public DataTable MostrarCategorias()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdCategoria, NombreCategoria FROM Categorias WHERE Estado = 1 ORDER BY NombreCategoria",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        // Insertar
        public void InsertarCategoria(string nombre)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Categorias (NombreCategoria, Estado) VALUES (@Nombre, 1)",
                    cn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar
        public void ActualizarCategoria(int id, string nombre)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Categorias SET NombreCategoria = @Nombre WHERE IdCategoria = @Id",
                    cn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Validar si existe
        public bool ExisteCategoria(string nombre)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Categorias WHERE NombreCategoria = @Nombre AND Estado = 1",
                    cn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0;
            }
        }

        // Desactivar (mejor que eliminar)
        public void CambiarEstado(int id, bool estado)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Categorias SET Estado = @Estado WHERE IdCategoria = @Id",
                    cn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}