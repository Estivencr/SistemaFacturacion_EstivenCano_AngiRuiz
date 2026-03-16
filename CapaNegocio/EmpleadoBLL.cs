using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class EmpleadoBLL
    {
        private readonly EmpleadoDAL _empleadoDal;

        public EmpleadoBLL()
        {
            _empleadoDal = new EmpleadoDAL();
        }

        public DataTable ListarEmpleados()
        {
            return _empleadoDal.ListarEmpleados();
        }

        public DataTable BuscarEmpleados(string texto)
        {
            return _empleadoDal.BuscarEmpleados(texto);
        }

        public DataRow ObtenerEmpleadoPorId(int idEmpleado)
        {
            if (idEmpleado <= 0) throw new Exception("Empleado invalido.");
            return _empleadoDal.ObtenerEmpleadoPorId(idEmpleado);
        }

        public void GuardarEmpleado(bool esEdicion, int idEmpleado, string nombreCompleto, string documento, string telefono, string cargo)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new Exception("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(cargo))
                throw new Exception("El cargo es obligatorio.");

            string doc = string.IsNullOrWhiteSpace(documento) ? null : documento.Trim();
            string tel = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();

            if (!esEdicion)
            {
                _empleadoDal.InsertarEmpleado(nombreCompleto.Trim(), doc, tel, cargo.Trim());
            }
            else
            {
                if (idEmpleado <= 0) throw new Exception("Empleado invalido.");
                _empleadoDal.ActualizarEmpleado(idEmpleado, nombreCompleto.Trim(), doc, tel, cargo.Trim());
            }
        }

        public void CambiarEstado(int idEmpleado, bool estado)
        {
            if (idEmpleado <= 0) throw new Exception("Empleado invalido.");
            _empleadoDal.CambiarEstado(idEmpleado, estado);
        }
    }
}

