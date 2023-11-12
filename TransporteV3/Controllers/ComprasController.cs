using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransporteV3.Entidades;

namespace TransporteV3.Controllers
{
    public class ComprasController : Controller
    {
        private readonly TAIProdContext _context;
        private readonly string cadenaSQL;

        
        public ComprasController(TAIProdContext context, IConfiguration config)
        {
            _context = context;
            cadenaSQL = config.GetConnectionString("cadenasql");
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {            
            var tAIProdContext = _context.Compras.Include(c => c.IdFormaPagoNavigation);
            return View(await tAIProdContext.ToListAsync());
        }



        // GET: ReportCompras
        public async Task<IActionResult> ReportCompras()
        {
            int totalCompras = _context.Compras.Count();
            ViewBag.TotalCompras = totalCompras;
            var tAIProdContext = _context.Compras.Include(c => c.IdFormaPagoNavigation);
            return View(await tAIProdContext.ToListAsync());
        }


        //Esport a excel
        public IActionResult Exportar_excel(DateTime fechainicio, DateTime fechafin)
        {
            DataTable tabla_compras = new DataTable();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand("sp_reporte_Compra", conexion);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechainicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechafin);

                    adapter.Fill(tabla_compras);
                }
            }

            using (var libro = new XLWorkbook())
            {
                tabla_compras.TableName = "Compras";
                var hoja = libro.Worksheets.Add(tabla_compras);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    var nombreExcel = string.Concat("Reporte compras", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }

        }


        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdFormaPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,Cantidad,Detalle,Precio,IdFormaPago,FechaCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", compra.IdFormaPago);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", compra.IdFormaPago);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,Cantidad,Detalle,Precio,IdFormaPago,FechaCompra")] Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdFormaPago"] = new SelectList(_context.FormasPagos, "IdFormaPago", "FormaPago", compra.IdFormaPago);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdFormaPagoNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'TAIProdContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return _context.Compras.Any(e => e.IdCompra == id);
        }
    }
}
