using Microsoft.AspNetCore.Mvc;
using MiPymes_Front_end_C_.Models;
using MiPymes_Front_end_C_.Services;
using System.Threading.Tasks;

namespace MiPymes_Front_end_C_.Controllers
{
    public class MiPymesController : Controller
    {
        private readonly ApiService _apiService;
        
        public MiPymesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Método para mostrar la lista de MiPYMES
        public async Task<IActionResult> Index(string searchTerm)
        {
            var miPymes = await _apiService.GetMiPymesAsync();

            // Validar que ninguna propiedad "Empresa" sea nula
            foreach (var miPyme in miPymes)
            {
                if (string.IsNullOrEmpty(miPyme.Empresa))
                {
                    miPyme.Empresa = "No disponible";
                }
            }

            // Filtrar empresas por el término de búsqueda
            if (!string.IsNullOrEmpty(searchTerm))
            {
                miPymes = miPymes
                    .Where(e => e.Empresa.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Pasar el término de búsqueda a la vista
            ViewBag.SearchTerm = searchTerm;

            return View(miPymes);
        }


        // Método para mostrar el formulario de creación
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Método para crear una nueva MiPyme
        [HttpPost]
        public async Task<IActionResult> Create(MiPyme newMiPyme)
        {
            if (ModelState.IsValid)
            {
                await _apiService.PostNewMiPymeAsync(newMiPyme);
                return RedirectToAction(nameof(Index));
            }
            return View(newMiPyme);
        }

        [HttpGet]
        // Acción para mostrar el formulario de edición de una MiPyme existente
        public async Task<IActionResult> Edit(string rnc)
        {
            var miPyme = await _apiService.GetMiPymeByRNC(rnc);

            if (miPyme == null)
            {
                return NotFound();
            }
            return View(miPyme);
        }

        [HttpPost]
        // Acción para manejar la actualización de una MiPyme existente (PUT)
        [HttpPost]
        public async Task<IActionResult> Edit(string rnc, MiPyme updatedMiPyme)
        {
            if (ModelState.IsValid)
            {
                await _apiService.PutMiPymeAsync(rnc, updatedMiPyme);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedMiPyme);
        }


        // Acción para mostrar el formulario de confirmación de eliminación de una MiPyme
        [HttpGet]
        public async Task<IActionResult> Delete(string rnc)
        {
            var miPyme = await _apiService.GetMiPymeByRNC(rnc);
            if (miPyme == null)
            {
                return NotFound();
            }
            return View(miPyme);
        }

        // Acción para manejar la eliminación de una MiPyme (DELETE)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string rnc)
        {
            var success = await _apiService.DeleteMiPymeAsync(rnc);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            // Si la eliminación falla, puedes volver a mostrar el formulario con un mensaje de error
            ModelState.AddModelError("", "Error al intentar eliminar la MiPyme.");
            var miPyme = await _apiService.GetMiPymeByRNC(rnc);
            if (miPyme == null)
            {
                return NotFound();
            }
            return View(miPyme);
        }

        //====================================================================================
        //Controller para empresas por RNC

        public IActionResult Consultar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Consultar(Empresa empresaData)
        {
            if (string.IsNullOrEmpty(empresaData.rnc))
            {
                ModelState.AddModelError("", "El RNC es obligatorio.");
                return View();
            }

            var empresaConsultada = await _apiService.GetEmpresaByRNCAsync(empresaData.rnc);
            if (empresaConsultada == null)
            {
                ModelState.AddModelError("", "No se ha encontrado ninguna empresa bajo el RNC suministrado");
                return View();
            }

            return View("Resultados", empresaConsultada);
        }

    }
}
