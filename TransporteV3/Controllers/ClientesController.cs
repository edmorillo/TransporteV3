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
    public class ClientesController : Controller
    {
        private readonly TAIProdContext _context;

        public ClientesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var tAIProdContext = _context.Clientes.Include(c => c.IdCondicionIvaNavigation).Include(c => c.IdProvinciaNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdCondicionIvaNavigation)
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["IdCondicionIva"] = new SelectList(_context.CondicionIvas, "IdCondicionIva", "CondicionIva1");
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombre,Apellido,RazonSocial,Cuit,IdProvincia,Direccion,CodigoPostal,Email,Telefono,IdCondicionIva,IngresosBrutos")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCondicionIva"] = new SelectList(_context.CondicionIvas, "IdCondicionIva", "CondicionIva1", cliente.IdCondicionIva);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", cliente.IdProvincia);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["IdCondicionIva"] = new SelectList(_context.CondicionIvas, "IdCondicionIva", "CondicionIva1", cliente.IdCondicionIva);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", cliente.IdProvincia);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Nombre,Apellido,RazonSocial,Cuit,IdProvincia,Direccion,CodigoPostal,Email,Telefono,IdCondicionIva,IngresosBrutos")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            ViewData["IdCondicionIva"] = new SelectList(_context.CondicionIvas, "IdCondicionIva", "CondicionIva1", cliente.IdCondicionIva);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", cliente.IdProvincia);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.IdCondicionIvaNavigation)
                .Include(c => c.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'TAIProdContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
