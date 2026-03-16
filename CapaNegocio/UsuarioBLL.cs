using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL;

        public UsuarioBLL()
        {
            usuarioDAL = new UsuarioDAL();
        }
        public DataTable MostrarEmpleados()
        {
            return usuarioDAL.MostrarEmpleados();
        }

        public DataTable ListarUsuarios()
        {
            return usuarioDAL.ListarUsuarios();
        }

        public DataRow ObtenerUsuarioPorId(int idUsuario)
        {
            if (idUsuario <= 0)
                throw new Exception("Usuario invalido.");

            return usuarioDAL.ObtenerUsuarioPorId(idUsuario);
        }

        public void CambiarEstado(int idUsuario, bool estado)
        {
            if (idUsuario <= 0)
                throw new Exception("Usuario invalido.");

            usuarioDAL.CambiarEstado(idUsuario, estado);
        }

        public void InsertarUsuario(int idEmpleado, string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Ingrese el usuario.");

            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("Ingrese la clave.");

            if (usuarioDAL.ExisteUsuario(usuario))
                throw new Exception("El usuario ya existe.");

            string hash = SeguridadHelper.GenerarHash(clave);

            usuarioDAL.InsertarUsuario(idEmpleado, usuario.Trim(), hash);
        }

        public void ActualizarUsuario(int idUsuario, int idEmpleado, string clave)
        {
            if (idUsuario <= 0)
                throw new Exception("Usuario inválido.");

            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("Ingrese la clave.");

            string hash = SeguridadHelper.GenerarHash(clave);

            usuarioDAL.ActualizarUsuario(idUsuario, idEmpleado, hash);
        }

        public DataTable Login(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("Ingrese el usuario.");

            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("Ingrese la contraseña.");

            string hash = SeguridadHelper.GenerarHash(clave);

            return usuarioDAL.Login(usuario.Trim(), hash);
        }
    }
}
