using Microsoft.AspNetCore.Mvc;
using SistemainventarioV6.Modelos;
using Microsoft.AspNetCore.Authorization;
using SistemainventarioV6.Utilidades;
using SistemainventarioV6.AccesoDatos.Repositorio;

namespace SistemainventarioV6.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _UnidadTrabajo;
        public BodegaController( IUnidadTrabajo unidadTrabajo)
        {
            _UnidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Upsert(int? id)
        {
            Bodega bodega = new Bodega();
            if (id == null)
            {
                //crear nueva bodega
                bodega.Estado = true;
                return View(bodega);
            }
            //existea actualizamos
            bodega = await _UnidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if (bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    await _UnidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Bodega creada Exitosamente";
                }
                else
                {
                   _UnidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Bodega actualizada Exitosamente";
                }
                await _UnidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
                  TempData[DS.Error] = "Error al grabar Bodega";
                  return View(bodega);
        }


        #region API

        [HttpGet]
            public async Task<IActionResult>ObtenerTodos()
            {
                var todos = await _UnidadTrabajo.Bodega.ObtenerTodos();
                return Json(new { data = todos });
            }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bodegaDb = await _UnidadTrabajo.Bodega.Obtener(id);
            if (bodegaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Bodega" });
            }
            _UnidadTrabajo.Bodega.Remover(bodegaDb);
            await _UnidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _UnidadTrabajo.Bodega.ObtenerTodos();
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


