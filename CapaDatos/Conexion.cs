using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        private string cadena =
            "Server=DESKTOP-TUL6SGR\\SQLEXPRESS;Database=SistemaVentasDB;Trusted_Connection=True;";

        public SqlConnection CrearConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}
