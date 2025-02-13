using Microsoft.AspNetCore.Mvc;
using SistemainventarioV6.Modelos;
using Microsoft.AspNetCore.Authorization;
using SistemainventarioV6.Utilidades;
using SistemainventarioV6.AccesoDatos.Repositorio;

namespace SistemainventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _UnidadTrabajo;
        public MarcaController( IUnidadTrabajo unidadTrabajo)
        {
            _UnidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Upsert(int? id)
        {
            Marca marca = new Marca();
            if (id == null)
            {
                //crear nueva Marca
                marca.Estado = true;
                return View(marca);
            }
            //existea actualizamos
            marca = await _UnidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    await _UnidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada Exitosamente";
                }
                else
                {
                   _UnidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada Exitosamente";
                }
                await _UnidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
                  TempData[DS.Error] = "Error al grabar Categoria";
                  return View(marca);
        }


        #region API

        [HttpGet]
            public async Task<IActionResult>ObtenerTodos()
            {
                var todos = await _UnidadTrabajo.Marca.ObtenerTodos();
                return Json(new { data = todos });
            }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marcaDb = await _UnidadTrabajo.Marca.Obtener(id);
            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Marca" });
            }
            _UnidadTrabajo.Marca.Remover(marcaDb);
            await _UnidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _UnidadTrabajo.Marca.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }

        #endregion
    }
}


