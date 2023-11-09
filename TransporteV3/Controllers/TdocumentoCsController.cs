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
    public class TdocumentoCsController : Controller
    {
        private readonly TAIProdContext _context;

        public TdocumentoCsController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: TdocumentoCs
        public async Task<IActionResult> Index()
        {
              return View(await _context.TdocumentoCs.ToListAsync());
        }

        // GET: TdocumentoCs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TdocumentoCs == null)
            {
                return NotFound();
            }

            var tdocumentoC = await _context.TdocumentoCs
                .FirstOrDefaultAsync(m => m.IdTdocuC == id);
            if (tdocumentoC == null)
            {
                return NotFound();
            }

            return View(tdocumentoC);
        }

        // GET: TdocumentoCs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TdocumentoCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTdocuC,Detalle")] TdocumentoC tdocumentoC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tdocumentoC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tdocumentoC);
        }

        // GET: TdocumentoCs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TdocumentoCs == null)
            {
                return NotFound();
            }

            var tdocumentoC = await _context.TdocumentoCs.FindAsync(id);
            if (tdocumentoC == null)
            {
                return NotFound();
            }
            return View(tdocumentoC);
        }

        // POST: TdocumentoCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTdocuC,Detalle")] TdocumentoC tdocumentoC)
        {
            if (id != tdocumentoC.IdTdocuC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tdocumentoC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdocumentoCExists(tdocumentoC.IdTdocuC))
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
            return View(tdocumentoC);
        }

        // GET: TdocumentoCs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TdocumentoCs == null)
            {
                return NotFound();
            }

            var tdocumentoC = await _context.TdocumentoCs
                .FirstOrDefaultAsync(m => m.IdTdocuC == id);
            if (tdocumentoC == null)
            {
                return NotFound();
            }

            return View(tdocumentoC);
        }

        // POST: TdocumentoCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TdocumentoCs == null)
            {
                return Problem("Entity set 'TAIProdContext.TdocumentoCs'  is null.");
            }
            var tdocumentoC = await _context.TdocumentoCs.FindAsync(id);
            if (tdocumentoC != null)
            {
                _context.TdocumentoCs.Remove(tdocumentoC);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdocumentoCExists(int id)
        {
          return _context.TdocumentoCs.Any(e => e.IdTdocuC == id);
        }
    }
}
