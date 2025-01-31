using Microsoft.AspNetCore.Mvc.Rendering;
using SistemainventarioV6.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemainventarioV6.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
     {
        void Actualizar(Producto producto);
          IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
    }
}
