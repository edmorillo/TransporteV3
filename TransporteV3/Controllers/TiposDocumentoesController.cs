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
    public class TiposDocumentoesController : Controller
    {
        private readonly TAIProdContext _context;

        public TiposDocumentoesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: TiposDocumentoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TiposDocumentos.ToListAsync());
        }

        // GET: TiposDocumentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TiposDocumentos == null)
            {
                return NotFound();
            }

            var tiposDocumento = await _context.TiposDocumentos
                .FirstOrDefaultAsync(m => m.IdTiposDocumentos == id);
            if (tiposDocumento == null)
            {
                return NotFound();
            }

            return View(tiposDocumento);
        }

        // GET: TiposDocumentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposDocumentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTiposDocumentos,TipoDocumento")] TiposDocumento tiposDocumento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposDocumento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposDocumento);
        }

        // GET: TiposDocumentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposDocumentos == null)
            {
                return NotFound();
            }

            var tiposDocumento = await _context.TiposDocumentos.FindAsync(id);
            if (tiposDocumento == null)
            {
                return NotFound();
            }
            return View(tiposDocumento);
        }

        // POST: TiposDocumentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTiposDocumentos,TipoDocumento")] TiposDocumento tiposDocumento)
        {
            if (id != tiposDocumento.IdTiposDocumentos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposDocumento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposDocumentoExists(tiposDocumento.IdTiposDocumentos))
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
            return View(tiposDocumento);
        }

        // GET: TiposDocumentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TiposDocumentos == null)
            {
                return NotFound();
            }

            var tiposDocumento = await _context.TiposDocumentos
                .FirstOrDefaultAsync(m => m.IdTiposDocumentos == id);
            if (tiposDocumento == null)
            {
                return NotFound();
            }

            return View(tiposDocumento);
        }

        // POST: TiposDocumentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TiposDocumentos == null)
            {
                return Problem("Entity set 'TAIProdContext.TiposDocumentos'  is null.");
            }
            var tiposDocumento = await _context.TiposDocumentos.FindAsync(id);
            if (tiposDocumento != null)
            {
                _context.TiposDocumentos.Remove(tiposDocumento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposDocumentoExists(int id)
        {
          return _context.TiposDocumentos.Any(e => e.IdTiposDocumentos == id);
        }
    }
}
