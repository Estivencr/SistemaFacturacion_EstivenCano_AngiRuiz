using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class EmpleadoDAL
    {
        private readonly Conexion _conexion;

        public EmpleadoDAL()
        {
            _conexion = new Conexion();
        }

        public DataTable ListarEmpleados()
        {
            var tabla = new DataTable();

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT IdEmpleado,
                             NombreCompleto,
                             Documento,
                             Telefono,
                             Cargo,
                             Estado,
                             FechaCreacion
                      FROM Empleados
                      ORDER BY IdEmpleado DESC",
                    cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public DataTable BuscarEmpleados(string texto)
        {
            var tabla = new DataTable();

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT IdEmpleado,
                             NombreCompleto,
                             Documento,
                             Telefono,
                             Cargo,
                             Estado,
                             FechaCreacion
                      FROM Empleados
                      WHERE NombreCompleto LIKE @Texto
                         OR Documento LIKE @Texto
                         OR Cargo LIKE @Texto
                      ORDER BY IdEmpleado DESC",
                    cn);

                cmd.Parameters.AddWithValue("@Texto", "%" + (texto ?? string.Empty) + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public DataRow ObtenerEmpleadoPorId(int idEmpleado)
        {
            var tabla = new DataTable();

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT TOP 1 IdEmpleado,
                                     NombreCompleto,
                                     Documento,
                                     Telefono,
                                     Cargo,
                                     Estado
                      FROM Empleados
                      WHERE IdEmpleado = @IdEmpleado",
                    cn);

                cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla.Rows.Count > 0 ? tabla.Rows[0] : null;
        }

        public void InsertarEmpleado(string nombreCompleto, string documento, string telefono, string cargo)
        {
            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Empleados (NombreCompleto, Documento, Telefono, Cargo, Estado)
                      VALUES (@NombreCompleto, @Documento, @Telefono, @Cargo, 1)",
                    cn);

                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", (object)documento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cargo", cargo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarEmpleado(int idEmpleado, string nombreCompleto, string documento, string telefono, string cargo)
        {
            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Empleados
                      SET NombreCompleto = @NombreCompleto,
                          Documento = @Documento,
                          Telefono = @Telefono,
                          Cargo = @Cargo
                      WHERE IdEmpleado = @IdEmpleado",
                    cn);

                cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Documento", (object)documento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cargo", cargo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(int idEmpleado, bool estado)
        {
            using (SqlConnection cn = _conexion.CrearConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Empleados SET Estado = @Estado WHERE IdEmpleado = @IdEmpleado",
                    cn);

                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

