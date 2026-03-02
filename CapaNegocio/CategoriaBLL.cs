using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CategoriaBLL
    {
        CategoriaDAL categoriaDAL = new CategoriaDAL();

        public DataTable MostrarCategorias()
        {
            return categoriaDAL.MostrarCategorias();
        }

        public void InsertarCategoria(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre de la categoría es obligatorio.");

            if (categoriaDAL.ExisteCategoria(nombre))
                throw new Exception("La categoría ya existe.");

            categoriaDAL.InsertarCategoria(nombre.Trim());
        }

        public void ActualizarCategoria(int id, string nombre)
        {
            if (id <= 0)
                throw new Exception("Categoría inválida.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es obligatorio.");

            categoriaDAL.ActualizarCategoria(id, nombre.Trim());
        }
    }
}
