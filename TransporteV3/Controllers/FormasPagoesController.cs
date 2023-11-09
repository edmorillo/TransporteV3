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
    public class FormasPagoesController : Controller
    {
        private readonly TAIProdContext _context;

        public FormasPagoesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: FormasPagoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.FormasPagos.ToListAsync());
        }

        // GET: FormasPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormasPagos == null)
            {
                return NotFound();
            }

            var formasPago = await _context.FormasPagos
                .FirstOrDefaultAsync(m => m.IdFormaPago == id);
            if (formasPago == null)
            {
                return NotFound();
            }

            return View(formasPago);
        }

        // GET: FormasPagoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormasPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormaPago,FormaPago")] FormasPago formasPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formasPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formasPago);
        }

        // GET: FormasPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormasPagos == null)
            {
                return NotFound();
            }

            var formasPago = await _context.FormasPagos.FindAsync(id);
            if (formasPago == null)
            {
                return NotFound();
            }
            return View(formasPago);
        }

        // POST: FormasPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFormaPago,FormaPago")] FormasPago formasPago)
        {
            if (id != formasPago.IdFormaPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formasPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormasPagoExists(formasPago.IdFormaPago))
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
            return View(formasPago);
        }

        // GET: FormasPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormasPagos == null)
            {
                return NotFound();
            }

            var formasPago = await _context.FormasPagos
                .FirstOrDefaultAsync(m => m.IdFormaPago == id);
            if (formasPago == null)
            {
                return NotFound();
            }

            return View(formasPago);
        }

        // POST: FormasPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormasPagos == null)
            {
                return Problem("Entity set 'TAIProdContext.FormasPagos'  is null.");
            }
            var formasPago = await _context.FormasPagos.FindAsync(id);
            if (formasPago != null)
            {
                _context.FormasPagos.Remove(formasPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormasPagoExists(int id)
        {
          return _context.FormasPagos.Any(e => e.IdFormaPago == id);
        }
    }
}
