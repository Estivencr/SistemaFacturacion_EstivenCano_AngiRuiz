using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class VentaDAL
    {
        private readonly Conexion _conexion;

        public VentaDAL()
        {
            _conexion = new Conexion();
        }

        // Inserta una venta y sus detalles dentro de una transaccion.
        // detalle: columnas requeridas -> IdProducto(int), Cantidad(int), PrecioUnitario(decimal), SubTotal(decimal)
        public int RegistrarVenta(int idCliente, int idEmpleado, decimal total, DataTable detalle)
        {
            if (detalle == null || detalle.Rows.Count == 0)
                throw new Exception("No hay productos para registrar.");

            using (SqlConnection cn = _conexion.CrearConexion())
            {
                cn.Open();

                using (SqlTransaction tx = cn.BeginTransaction())
                {
                    try
                    {
                        int idVenta;

                        using (SqlCommand cmdVenta = new SqlCommand(
                            @"INSERT INTO Ventas (IdCliente, IdEmpleado, Total, Estado)
                              OUTPUT INSERTED.IdVenta
                              VALUES (@IdCliente, @IdEmpleado, @Total, 1);",
                            cn, tx))
                        {
                            cmdVenta.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                            cmdVenta.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                            cmdVenta.Parameters.Add("@Total", SqlDbType.Decimal).Value = total;
                            cmdVenta.Parameters["@Total"].Precision = 18;
                            cmdVenta.Parameters["@Total"].Scale = 2;

                            idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                        }

                        foreach (DataRow row in detalle.Rows)
                        {
                            int idProducto = Convert.ToInt32(row["IdProducto"]);
                            int cantidad = Convert.ToInt32(row["Cantidad"]);
                            decimal precioUnitario = Convert.ToDecimal(row["PrecioUnitario"]);
                            decimal subTotal = Convert.ToDecimal(row["SubTotal"]);

                            // Verificar stock
                            int stockActual;
                            using (SqlCommand cmdStock = new SqlCommand(
                                "SELECT Stock FROM Productos WHERE IdProducto = @IdProducto",
                                cn, tx))
                            {
                                cmdStock.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                                object result = cmdStock.ExecuteScalar();
                                stockActual = result == null ? 0 : Convert.ToInt32(result);
                            }

                            if (cantidad <= 0)
                                throw new Exception("Cantidad invalida para el producto seleccionado.");

                            if (stockActual < cantidad)
                                throw new Exception("Stock insuficiente para el producto seleccionado.");

                            // Insertar detalle
                            using (SqlCommand cmdDet = new SqlCommand(
                                @"INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal)
                                  VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @SubTotal);",
                                cn, tx))
                            {
                                cmdDet.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;
                                cmdDet.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                                cmdDet.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;

                                cmdDet.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = precioUnitario;
                                cmdDet.Parameters["@PrecioUnitario"].Precision = 18;
                                cmdDet.Parameters["@PrecioUnitario"].Scale = 2;

                                cmdDet.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = subTotal;
                                cmdDet.Parameters["@SubTotal"].Precision = 18;
                                cmdDet.Parameters["@SubTotal"].Scale = 2;

                                cmdDet.ExecuteNonQuery();
                            }

                            // Descontar stock
                            using (SqlCommand cmdUpd = new SqlCommand(
                                "UPDATE Productos SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto",
                                cn, tx))
                            {
                                cmdUpd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                                cmdUpd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                                cmdUpd.ExecuteNonQuery();
                            }
                        }

                        tx.Commit();
                        return idVenta;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

