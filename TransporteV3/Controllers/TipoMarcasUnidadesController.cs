using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransporteV3.Entidades;

namespace TransporteV3.Controllers
{
    public class TipoMarcasUnidadesController : Controller
    {
        private readonly TAIProdContext _context;

        public TipoMarcasUnidadesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: TipoMarcasUnidades
        public async Task<IActionResult> Index()
        {
              return View(await _context.TipoMarcasUnidades.ToListAsync());
        }

        // GET: TipoMarcasUnidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoMarcasUnidades == null)
            {
                return NotFound();
            }

            var tipoMarcasUnidade = await _context.TipoMarcasUnidades
                .FirstOrDefaultAsync(m => m.IdTipoMarcaUnidad == id);
            if (tipoMarcasUnidade == null)
            {
                return NotFound();
            }

            return View(tipoMarcasUnidade);
        }

        // GET: TipoMarcasUnidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMarcasUnidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoMarcaUnidad,TipoMarcaUnidad")] TipoMarcasUnidade tipoMarcasUnidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMarcasUnidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMarcasUnidade);
        }

        // GET: TipoMarcasUnidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoMarcasUnidades == null)
            {
                return NotFound();
            }

            var tipoMarcasUnidade = await _context.TipoMarcasUnidades.FindAsync(id);
            if (tipoMarcasUnidade == null)
            {
                return NotFound();
            }
            return View(tipoMarcasUnidade);
        }

        // POST: TipoMarcasUnidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoMarcaUnidad,TipoMarcaUnidad")] TipoMarcasUnidade tipoMarcasUnidade)
        {
            if (id != tipoMarcasUnidade.IdTipoMarcaUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMarcasUnidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMarcasUnidadeExists(tipoMarcasUnidade.IdTipoMarcaUnidad))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMarcasUnidade);
        }


        private bool TipoMarcasUnidadeExists(int id)
        {
            return _context.TipoMarcasUnidades.Any(e => e.IdTipoMarcaUnidad == id);
        }

        // GET: TipoMarcasUnidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoMarcasUnidades == null)
            {
                return NotFound();
            }

            var tipoMarcasUnidade = await _context.TipoMarcasUnidades
                .FirstOrDefaultAsync(m => m.IdTipoMarcaUnidad == id);
            if (tipoMarcasUnidade == null)
            {
                return NotFound();
            }

            return View(tipoMarcasUnidade);
        }

        // POST: TipoMarcasUnidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.TipoMarcasUnidades == null)
        //    {
        //        return Problem("Entity set 'TAIProdContext.TipoMarcasUnidades'  is null.");
        //    }
        //    var tipoMarcasUnidade = await _context.TipoMarcasUnidades.FindAsync(id);
        //    if (tipoMarcasUnidade != null)
        //    {
        //        _context.TipoMarcasUnidades.Remove(tipoMarcasUnidade);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoMarcasUnidade = await _context.TipoMarcasUnidades.FindAsync(id);

            if (tipoMarcasUnidade == null)
            {
                return NotFound();
            }

            try
            {
                // Verificar dependencias antes de eliminar
                if (ExistenDependencias(id))
                {
                    TempData["ErrorMessage"] = "No se puede eliminar el dato, esta siendo usado en tipo de unidades.";
                    return RedirectToAction(nameof(Index));
                }

                _context.TipoMarcasUnidades.Remove(tipoMarcasUnidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                // Manejar cualquier error al eliminar
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar el estado.";
                // Log del error ex.Message
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ExistenDependencias(int tipoMarcasUnidadeId)
        {
            // Verificar si existen dependencias (por ejemplo, con choferes)
            return _context.TipoUnidades.Any(c => c.IdTipoMarcaUnidades == tipoMarcasUnidadeId);
        }

        
    }
}
