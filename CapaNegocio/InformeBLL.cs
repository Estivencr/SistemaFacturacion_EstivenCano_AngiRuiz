using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class InformeBLL
    {
        private readonly InformeDAL _informeDal;

        public InformeBLL()
        {
            _informeDal = new InformeDAL();
        }

        public DataTable ObtenerVentas(DateTime? desde, DateTime? hasta, int? idCliente, int? idEmpleado)
        {
            if (desde.HasValue && hasta.HasValue && hasta.Value.Date < desde.Value.Date)
                throw new Exception("La fecha 'Hasta' no puede ser menor que 'Desde'.");

            return _informeDal.ObtenerVentas(desde, hasta, idCliente, idEmpleado);
        }

        public DataTable ObtenerDetalleVenta(int idVenta)
        {
            if (idVenta <= 0) throw new Exception("Venta invalida.");
            return _informeDal.ObtenerDetalleVenta(idVenta);
        }
    }
}

