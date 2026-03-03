using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ClienteDAL
    {
        private Conexion conexion;

        public ClienteDAL()
        {
            conexion = new Conexion();
        }

        public DataTable MostrarClientes()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT IdCliente,
                             NombreCompleto,
                             Documento,
                             Telefono,
                             Email,
                             Direccion,
                             Estado, 
                             FechaCreacion
                      FROM Clientes",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public DataTable BuscarCliente(string texto)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT * FROM Clientes
              WHERE NombreCompleto LIKE @Texto 
              OR Documento LIKE @Texto", // Cambiado 'Nombre' por 'NombreCompleto'
                    cn);

                cmd.Parameters.AddWithValue("@Texto", "%" + texto + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }
            return tabla;
        }

        public void CambiarEstado(int idCliente, bool estado)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Clientes
                      SET Estado=@Estado
                      WHERE IdCliente=@IdCliente",
                    cn);

                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable ObtenerClientePorId(int idCliente)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Clientes WHERE IdCliente=@IdCliente",
                    cn);

                cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public void InsertarCliente(string nombreCompleto,
                             string documento,
                             string telefono,
                             string email,
                             string direccion)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Clientes
              (NombreCompleto, Documento, Telefono, Email, Direccion)
              VALUES
              (@NombreCompleto, @Documento, @Telefono, @Email, @Direccion)",
                    cn);

                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCliente(int idCliente,
                              string nombreCompleto,
                              string documento,
                              string telefono,
                              string email,
                              string direccion)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Clientes SET
              NombreCompleto=@NombreCompleto,
              Documento=@Documento,
              Telefono=@Telefono,
              Email=@Email,
              Direccion=@Direccion
              WHERE IdCliente=@IdCliente",
                    cn);

                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
