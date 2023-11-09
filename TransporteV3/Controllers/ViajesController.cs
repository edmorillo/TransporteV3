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
    public class ViajesController : Controller
    {
        private readonly TAIProdContext _context;

        public ViajesController(TAIProdContext context)
        {
            _context = context;
        }

        // GET: Viajes
        public async Task<IActionResult> Index()
        {
            var tAIProdContext = _context.Viajes.Include(v => v.IdChoferNavigation).Include(v => v.IdClienteNavigation).Include(v => v.IdFormaPagoNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: ReporteViajes
        public async Task<IActionResult> ReportViajes()
        {
            var tAIProdContext = _context.Viajes.Include(v => v.IdChoferNavigation).Include(v => v.IdClienteNavigation).Include(v => v.IdFormaPagoNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: Viajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.IdChoferNavigation)
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdFormaPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdViajes == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // GET: Viajes/Create
        public IActionResult Create()

        {
            var _context2 = _context;
            var listaChofer = _context2.Choferes.Where(Chofere => Chofere.IdEstado == 1).ToList();

            //var _context2 = _context;
            //var listaChofer = _context2.Choferes.Where(Chofere => Chofere.Cuil == "1234567890").ToList();

            //using (var db = new TAIProdContext())
            //{
            //    var choferesActivos = (from chofer in db.Choferes
            //                           join Estado in db.Estados
            //                           on chofer.IdEstado equals Estado.IdEstado
            //                           where Estado.Estado1 == "1"
            //                           select chofer).ToList();

            ViewData["IdChofer"] = new SelectList(listaChofer, "IdChofer", "Nombre");
                ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre");
                ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago");
                
                return View();
            //}
        }

        // POST: Viajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdViajes,Viajes,Origen,Destino,IdChofer,IdCliente,Tarifa,IdFormaPago,Escobrado,Detalle,Remito,Ncontenedor,EsFacturado,Entidad,Nfactura")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdChofer"] = new SelectList(_context.Choferes, "IdChofer", "Nombre", viaje.IdChofer);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", viaje.IdCliente);
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", viaje.IdFormaPago);
            return View(viaje);
        }

        // GET: Viajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }
            ViewData["IdChofer"] = new SelectList(_context.Choferes, "IdChofer", "Nombre", viaje.IdChofer);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", viaje.IdCliente);
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", viaje.IdFormaPago);
            return View(viaje);
        }

        // POST: Viajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdViajes,Viajes,Origen,Destino,IdChofer,IdCliente,Tarifa,IdFormaPago,Escobrado,Detalle,Remito,Ncontenedor,EsFacturado,Entidad,Nfactura")] Viaje viaje)
        {
            if (id != viaje.IdViajes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViajeExists(viaje.IdViajes))
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
            ViewData["IdChofer"] = new SelectList(_context.Choferes, "IdChofer", "Nombre", viaje.IdChofer);
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "IdCliente", "Nombre", viaje.IdCliente);
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", viaje.IdFormaPago);
            return View(viaje);
        }

        // GET: Viajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.IdChoferNavigation)
                .Include(v => v.IdClienteNavigation)
                .Include(v => v.IdFormaPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdViajes == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Viajes == null)
            {
                return Problem("Entity set 'TAIProdContext.Viajes'  is null.");
            }
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje != null)
            {
                _context.Viajes.Remove(viaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViajeExists(int id)
        {
          return _context.Viajes.Any(e => e.IdViajes == id);
        }
    }
}
