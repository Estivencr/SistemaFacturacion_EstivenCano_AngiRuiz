using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class InformeDAL
    {
        private readonly Conexion _conexion;

        public InformeDAL()
        {
            _conexion = new Conexion();
        }

        public DataTable ObtenerVentas(DateTime? desde, DateTime? hasta, int? idCliente, int? idEmpleado)
        {
            var dt = new DataTable();

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                var sql = new StringBuilder();
                sql.Append(@"
SELECT
    V.IdVenta,
    V.Fecha,
    V.Total,
    C.NombreCompleto AS Cliente,
    E.NombreCompleto AS Empleado
FROM Ventas V
INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
INNER JOIN Empleados E ON V.IdEmpleado = E.IdEmpleado
WHERE V.Estado = 1
");

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    if (desde.HasValue)
                    {
                        sql.Append(" AND V.Fecha >= @Desde");
                        cmd.Parameters.Add("@Desde", SqlDbType.DateTime).Value = desde.Value;
                    }

                    if (hasta.HasValue)
                    {
                        // Incluye todo el dia "hasta"
                        DateTime hastaEnd = hasta.Value.Date.AddDays(1);
                        sql.Append(" AND V.Fecha < @Hasta");
                        cmd.Parameters.Add("@Hasta", SqlDbType.DateTime).Value = hastaEnd;
                    }

                    if (idCliente.HasValue && idCliente.Value > 0)
                    {
                        sql.Append(" AND V.IdCliente = @IdCliente");
                        cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente.Value;
                    }

                    if (idEmpleado.HasValue && idEmpleado.Value > 0)
                    {
                        sql.Append(" AND V.IdEmpleado = @IdEmpleado");
                        cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado.Value;
                    }

                    sql.Append(" ORDER BY V.Fecha DESC, V.IdVenta DESC;");
                    cmd.CommandText = sql.ToString();

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable ObtenerDetalleVenta(int idVenta)
        {
            var dt = new DataTable();

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                using (SqlCommand cmd = new SqlCommand(@"
SELECT
    D.IdDetalle,
    D.IdProducto,
    P.Nombre AS Producto,
    D.Cantidad,
    D.PrecioUnitario,
    D.SubTotal
FROM DetalleVenta D
INNER JOIN Productos P ON D.IdProducto = P.IdProducto
WHERE D.IdVenta = @IdVenta
ORDER BY D.IdDetalle;", cn))
                {
                    cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}

