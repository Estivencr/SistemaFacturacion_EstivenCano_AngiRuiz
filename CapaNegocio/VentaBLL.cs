using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class VentaBLL
    {
        private readonly VentaDAL _ventaDal;

        public VentaBLL()
        {
            _ventaDal = new VentaDAL();
        }

        public int RegistrarVenta(int idCliente, int idEmpleado, List<VentaDetalleItem> items)
        {
            if (idCliente <= 0) throw new Exception("Cliente invalido.");
            if (idEmpleado <= 0) throw new Exception("Empleado invalido.");
            if (items == null || items.Count == 0) throw new Exception("Agregue productos a la factura.");

            var dt = new DataTable();
            dt.Columns.Add("IdProducto", typeof(int));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("PrecioUnitario", typeof(decimal));
            dt.Columns.Add("SubTotal", typeof(decimal));

            decimal total = 0m;
            foreach (var item in items)
            {
                if (item.IdProducto <= 0) throw new Exception("Producto invalido.");
                if (item.Cantidad <= 0) throw new Exception("Cantidad invalida.");
                if (item.PrecioUnitario <= 0) throw new Exception("Precio invalido.");

                decimal sub = item.Cantidad * item.PrecioUnitario;
                total += sub;
                dt.Rows.Add(item.IdProducto, item.Cantidad, item.PrecioUnitario, sub);
            }

            if (total <= 0m) throw new Exception("Total invalido.");

            return _ventaDal.RegistrarVenta(idCliente, idEmpleado, total, dt);
        }
    }

    public class VentaDetalleItem
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}

