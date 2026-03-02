using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CategoriaDAL
    {
        private string conexion = "TU_CADENA_DE_CONEXION";

        public DataTable MostrarCategorias()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdCategoria, NombreCategoria FROM Categorias WHERE Estado = 1 ORDER BY NombreCategoria",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public void InsertarCategoria(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Categorias (NombreCategoria) VALUES (@Nombre)",
                    cn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCategoria(int id, string nombre)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
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

        public bool ExisteCategoria(string nombre)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Categorias WHERE NombreCategoria = @Nombre AND Estado = 1",
                    cn);

                cmd.Parameters.AddWithValue("@Nombre", nombre);

                cn.Open();
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
