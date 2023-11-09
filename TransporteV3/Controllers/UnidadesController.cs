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
    public class UnidadesController : Controller
    {
        private readonly TAIProdContext _context;

        public UnidadesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: Unidades
        public async Task<IActionResult> Index()
        {
            var tAIProdContext = _context.Unidades.Include(u => u.IdNeumaticoNavigation).Include(u => u.IdTipoUnidadNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: ReporteUnidades
        public async Task<IActionResult> ReportUnidades()
        {
            var tAIProdContext = _context.Unidades.Include(u => u.IdNeumaticoNavigation).Include(u => u.IdTipoUnidadNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: Unidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Unidades == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .Include(u => u.IdNeumaticoNavigation)
                .Include(u => u.IdTipoUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // GET: Unidades/Create
        public IActionResult Create()
        {
            ViewData["IdNeumatico"] = new SelectList(_context.Neumaticos, "IdNeumatico", "Marca");
            ViewData["IdTipoUnidad"] = new SelectList(_context.TipoUnidades, "IdTipoUnidad", "Detalle");
            return View();
        }

        // POST: Unidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnidad,Matricula,Chasis,Modelo,Año,CapacidadCarga,IdTipoUnidad,IdNeumatico,Kilometros,FechaMantenimiento,FechaCompra,VencimientoUnidad")] Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNeumatico"] = new SelectList(_context.Neumaticos, "IdNeumatico", "Marca", unidade.IdNeumatico);
            ViewData["IdTipoUnidad"] = new SelectList(_context.TipoUnidades, "IdTipoUnidad", "Detalle", unidade.IdTipoUnidad);
            return View(unidade);
        }

        // GET: Unidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Unidades == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade == null)
            {
                return NotFound();
            }
            ViewData["IdNeumatico"] = new SelectList(_context.Neumaticos, "IdNeumatico", "Marca", unidade.IdNeumatico);
            ViewData["IdTipoUnidad"] = new SelectList(_context.TipoUnidades, "IdTipoUnidad", "Detalle", unidade.IdTipoUnidad);
            return View(unidade);
        }

        // POST: Unidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUnidad,Matricula,Chasis,Modelo,Año,CapacidadCarga,IdTipoUnidad,IdNeumatico,Kilometros,FechaMantenimiento,FechaCompra,VencimientoUnidad")] Unidade unidade)
        {
            if (id != unidade.IdUnidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeExists(unidade.IdUnidad))
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
            ViewData["IdNeumatico"] = new SelectList(_context.Neumaticos, "IdNeumatico", "Marca", unidade.IdNeumatico);
            ViewData["IdTipoUnidad"] = new SelectList(_context.TipoUnidades, "IdTipoUnidad", "Detalle", unidade.IdTipoUnidad);
            return View(unidade);
        }

        // GET: Unidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Unidades == null)
            {
                return NotFound();
            }

            var unidade = await _context.Unidades
                .Include(u => u.IdNeumaticoNavigation)
                .Include(u => u.IdTipoUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidade == null)
            {
                return NotFound();
            }

            return View(unidade);
        }

        // POST: Unidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Unidades == null)
            {
                return Problem("Entity set 'TAIProdContext.Unidades'  is null.");
            }
            var unidade = await _context.Unidades.FindAsync(id);
            if (unidade != null)
            {
                _context.Unidades.Remove(unidade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeExists(int id)
        {
          return _context.Unidades.Any(e => e.IdUnidad == id);
        }
    }
}
