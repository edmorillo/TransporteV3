using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransporteV3.Entidades;

namespace TransporteV3.Controllers
{
    public class TipoUnidadesController : Controller
    {
        private readonly TAIProdContext _context;

        public TipoUnidadesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: TipoUnidades
        public async Task<IActionResult> Index()
        {
            var tAIProdContext = _context.TipoUnidades.Include(t => t.IdTipoMarcaUnidadesNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: TipoUnidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoUnidades == null)
            {
                return NotFound();
            }

            var tipoUnidade = await _context.TipoUnidades
                .Include(t => t.IdTipoMarcaUnidadesNavigation)
                .FirstOrDefaultAsync(m => m.IdTipoUnidad == id);
            if (tipoUnidade == null)
            {
                return NotFound();
            }

            return View(tipoUnidade);
        }

        // GET: TipoUnidades/Create
        public IActionResult Create()
        {
            ViewData["IdTipoMarcaUnidades"] = new SelectList(_context.TipoMarcasUnidades, "IdTipoMarcaUnidad", "TipoMarcaUnidad");
            return View();
        }

        // POST: TipoUnidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoUnidad,Detalle,IdTipoMarcaUnidades")] TipoUnidade tipoUnidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoUnidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoMarcaUnidades"] = new SelectList(_context.TipoMarcasUnidades, "IdTipoMarcaUnidad", "TipoMarcaUnidad", tipoUnidade.IdTipoMarcaUnidades);
            return View(tipoUnidade);
        }

        // GET: TipoUnidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoUnidades == null)
            {
                return NotFound();
            }

            var tipoUnidade = await _context.TipoUnidades.FindAsync(id);
            if (tipoUnidade == null)
            {
                return NotFound();
            }
            ViewData["IdTipoMarcaUnidades"] = new SelectList(_context.TipoMarcasUnidades, "IdTipoMarcaUnidad", "TipoMarcaUnidad", tipoUnidade.IdTipoMarcaUnidades);
            return View(tipoUnidade);
        }

        // POST: TipoUnidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoUnidad,Detalle,IdTipoMarcaUnidades")] TipoUnidade tipoUnidade)
        {
            if (id != tipoUnidade.IdTipoUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoUnidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUnidadeExists(tipoUnidade.IdTipoUnidad))
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
            ViewData["IdTipoMarcaUnidades"] = new SelectList(_context.TipoMarcasUnidades, "IdTipoMarcaUnidad", "TipoMarcaUnidad", tipoUnidade.IdTipoMarcaUnidades);
            return View(tipoUnidade);
        }


        private bool TipoUnidadeExists(int id)
        {
            return _context.TipoUnidades.Any(e => e.IdTipoUnidad == id);
        }

        // GET: TipoUnidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoUnidades == null)
            {
                return NotFound();
            }

            var tipoUnidade = await _context.TipoUnidades
                .Include(t => t.IdTipoMarcaUnidadesNavigation)
                .FirstOrDefaultAsync(m => m.IdTipoUnidad == id);
            if (tipoUnidade == null)
            {
                return NotFound();
            }

            return View(tipoUnidade);
        }

        // POST: TipoUnidades/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.TipoUnidades == null)
        //    {
        //        return Problem("Entity set 'TAIProdContext.TipoUnidades'  is null.");
        //    }
        //    var tipoUnidade = await _context.TipoUnidades.FindAsync(id);
        //    if (tipoUnidade != null)
        //    {
        //        _context.TipoUnidades.Remove(tipoUnidade);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoUnidade = await _context.TipoUnidades.FindAsync(id);

            if (tipoUnidade == null)
            {
                return NotFound();
            }

            try
            {
                // Verificar dependencias antes de eliminar
                if (ExistenDependencias(id))
                {
                    TempData["ErrorMessage"] = "No se puede eliminar el dato, esta siendo usado en unidades.";
                    return RedirectToAction(nameof(Index));
                }

                _context.TipoUnidades.Remove(tipoUnidade);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Dato eliminado correctamente.";
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

        private bool ExistenDependencias(int tipoUnidadeId)
        {
            // Verificar si existen dependencias (por ejemplo, con choferes)
            return _context.Unidades.Any(t => t.IdTipoUnidad == tipoUnidadeId);
        }


        
    }
}
