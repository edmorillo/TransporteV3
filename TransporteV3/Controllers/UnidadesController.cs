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
    public class UnidadesController : Controller
    {
        private readonly TAIProdContext _context;
        private readonly string cadenaSQL;

        public UnidadesController(TAIProdContext context, IConfiguration config)
        {
            _context = context;
            cadenaSQL = config.GetConnectionString("cadenasql");
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

            int totalUnidades = _context.Unidades.Count();
            ViewBag.TotalUnidades = totalUnidades;
            var tAIProdContext = _context.Unidades.Include(u => u.IdNeumaticoNavigation).Include(u => u.IdTipoUnidadNavigation);
            return View(await tAIProdContext.ToListAsync());
        }


        //Esport a excel
        public IActionResult Exportar_excel(DateTime fechainicio, DateTime fechafin)
        {
            DataTable tabla_unidades = new DataTable();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand("sp_reporte_Unidades", conexion);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechainicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechafin);

                    adapter.Fill(tabla_unidades);
                }
            }

            using (var libro = new XLWorkbook())
            {
                tabla_unidades.TableName = "Unidades";
                var hoja = libro.Worksheets.Add(tabla_unidades);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    var nombreExcel = string.Concat("Reporte unidades", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }

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
