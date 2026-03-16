using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class UsuarioDAL
    {
        private Conexion conexion;

        public UsuarioDAL()
        {
            conexion = new Conexion();
        }

        public DataTable MostrarEmpleados()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdEmpleado, NombreCompleto FROM Empleados WHERE Estado = 1",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public DataTable ListarUsuarios()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT U.IdUsuario,
                             U.IdEmpleado,
                             E.NombreCompleto,
                             U.Usuario,
                             U.Rol,
                             U.Estado,
                             U.FechaCreacion
                      FROM Usuarios U
                      INNER JOIN Empleados E ON U.IdEmpleado = E.IdEmpleado
                      ORDER BY U.IdUsuario DESC",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public DataRow ObtenerUsuarioPorId(int idUsuario)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 1 U.IdUsuario,
                                     U.IdEmpleado,
                                     E.NombreCompleto,
                                     U.Usuario,
                                     U.Rol,
                                     U.Estado
                      FROM Usuarios U
                      INNER JOIN Empleados E ON U.IdEmpleado = E.IdEmpleado
                      WHERE U.IdUsuario = @IdUsuario",
                    cn);

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla.Rows.Count > 0 ? tabla.Rows[0] : null;
        }

        public void CambiarEstado(int idUsuario, bool estado)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Usuarios SET Estado = @Estado WHERE IdUsuario = @IdUsuario",
                    cn);

                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool ExisteUsuario(string usuario)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Usuarios WHERE Usuario=@Usuario",
                    cn);

                cmd.Parameters.AddWithValue("@Usuario", usuario);

                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public void InsertarUsuario(int idEmpleado, string usuario, string hash)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Usuarios(IdEmpleado,Usuario,ClaveHash)
                  VALUES(@IdEmpleado,@Usuario,@ClaveHash)",
                    cn);

                cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@ClaveHash", hash);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarUsuario(int idUsuario, int idEmpleado, string hash)
        {
            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Usuarios 
                  SET IdEmpleado=@IdEmpleado,
                      ClaveHash=@ClaveHash
                  WHERE IdUsuario=@IdUsuario",
                    cn);

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@ClaveHash", hash);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable Login(string usuario, string hash)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT U.IdUsuario,
                     E.NombreCompleto,
                     U.Usuario
              FROM Usuarios U
              INNER JOIN Empleados E ON U.IdEmpleado = E.IdEmpleado
              WHERE U.Usuario = @Usuario
              AND U.ClaveHash = @ClaveHash
              AND U.Estado = 1",
                    cn);

                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@ClaveHash", hash);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }
    }   
}
