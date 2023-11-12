using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransporteV3.Entidades;

namespace TransporteV3.Controllers
{
    public class ChoferesController : Controller
    {
        private readonly TAIProdContext _context;
        private readonly string cadenaSQL;


        public ChoferesController(TAIProdContext context, IConfiguration config)
        {
            _context = context;
            cadenaSQL = config.GetConnectionString("cadenasql");
        }

        // GET: Choferes
        public async Task<IActionResult> Index()
        {
            var tAIProdContext = _context.Choferes.Include(c => c.IdEstadoNavigation).Include(c => c.IdProvinciaNavigation).Include(c => c.IdTdocuCNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        // GET: Total de Choferes
        //public async Task<IActionResult> TotalChoferesService()
        //{
        //    int totalChoferes = _context.Choferes.Count();
        //    ViewBag.TotalChoferes = totalChoferes;
        //    var tAIProdContext = _context.Choferes.Include(c => c.IdEstadoNavigation).Include(c => c.IdProvinciaNavigation).Include(c => c.IdTdocuCNavigation);
        //    return View(await tAIProdContext.ToListAsync());
        //}

        //ReportChoferes
        public async Task<IActionResult> ReportChoferes()
        {
            int totalChoferes = _context.Choferes.Count();
            ViewBag.TotalChoferes = totalChoferes;
            var tAIProdContext = _context.Choferes.Include(c => c.IdEstadoNavigation).Include(c => c.IdProvinciaNavigation).Include(c => c.IdTdocuCNavigation);
            return View(await tAIProdContext.ToListAsync());
        }

        //Esport a excel
        public IActionResult Exportar_excel(DateTime fechainicio, DateTime fechafin)
        {

            DataTable tabla_choferes = new DataTable();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                using (var adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = new SqlCommand("sp_reporte_Choferes", conexion);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechainicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechafin);

                    adapter.Fill(tabla_choferes);
                }
            }

            using (var libro = new XLWorkbook())
            {
                tabla_choferes.TableName = "Choferes";
                var hoja = libro.Worksheets.Add(tabla_choferes);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    var nombreExcel = string.Concat("Reporte choferes", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }

        }

        //prueba

        //public IActionResult Exportar_excel(DateTime fechainicio, DateTime fechafin)
        //{
        //    try
        //    {
        //        DataTable tabla_choferes = new DataTable();

        //        using (var conexion = new SqlConnection(cadenaSQL))
        //        {
        //            conexion.Open();
        //            using (var adapter = new SqlDataAdapter())
        //            {
        //                adapter.SelectCommand = new SqlCommand("sp_reporte_Choferes", conexion);
        //                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //                adapter.SelectCommand.Parameters.AddWithValue("@FechaInicio", fechainicio);
        //                adapter.SelectCommand.Parameters.AddWithValue("@FechaFin", fechafin);

        //                adapter.Fill(tabla_choferes);
        //            }
        //        }

        //        if (tabla_choferes.Rows.Count == 0)
        //        {
        //            // No hay datos, lanzar una excepción personalizada
        //            throw new InvalidOperationException("No hay datos disponibles para las fechas seleccionadas.");
        //        }

        //        using (var libro = new XLWorkbook())
        //        {
        //            tabla_choferes.TableName = "Choferes";
        //            var hoja = libro.Worksheets.Add(tabla_choferes);
        //            hoja.ColumnsUsed().AdjustToContents();

        //            using (var memoria = new MemoryStream())
        //            {
        //                libro.SaveAs(memoria);

        //                var nombreExcel = string.Concat("Reporte choferes", DateTime.Now.ToString(), ".xlsx");
        //                return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Capturar la excepción y devolver un mensaje de error
        //        return BadRequest($"Error al exportar el archivo Excel: {ex.Message}");
        //        //return RedirectToAction("Listado",
        //        //routeValues: new { mensaje = "Los datos ingresados, no tiene el formato correcto "  });
        //    }
        //}




            // GET: Choferes/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofere = await _context.Choferes
                .Include(c => c.IdEstadoNavigation)
                .Include(c => c.IdProvinciaNavigation)
                .Include(c => c.IdTdocuCNavigation)
                .FirstOrDefaultAsync(m => m.IdChofer == id);
            if (chofere == null)
            {
                return NotFound();
            }

            return View(chofere);
        }

        // GET: Choferes/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Estado1");
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia");
            ViewData["IdTdocuC"] = new SelectList(_context.TdocumentoCs, "IdTdocuC", "Detalle");
            return View();
        }

        // POST: Choferes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChofer,Nombre,Apellido,FechaNacimiento,IdTdocuC,Ndocumento,NuTramite,Cuil,IdProvincia,Direccion,Email,Celular,FechaAlta,FechaBaja,IdEstado")] Chofere chofere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chofere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Estado1", chofere.IdEstado);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", chofere.IdProvincia);
            ViewData["IdTdocuC"] = new SelectList(_context.TdocumentoCs, "IdTdocuC", "Detalle", chofere.IdTdocuC);
            return View(chofere);
        }

        // GET: Choferes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofere = await _context.Choferes.FindAsync(id);
            if (chofere == null)
            {
                return NotFound();
            }
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Estado1", chofere.IdEstado);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", chofere.IdProvincia);
            ViewData["IdTdocuC"] = new SelectList(_context.TdocumentoCs, "IdTdocuC", "Detalle", chofere.IdTdocuC);
            return View(chofere);
        }

        // POST: Choferes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChofer,Nombre,Apellido,FechaNacimiento,IdTdocuC,Ndocumento,NuTramite,Cuil,IdProvincia,Direccion,Email,Celular,FechaAlta,FechaBaja,IdEstado")] Chofere chofere)
        {
            if (id != chofere.IdChofer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chofere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChofereExists(chofere.IdChofer))
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
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "Estado1", chofere.IdEstado);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "IdProvincia", "Provincia", chofere.IdProvincia);
            ViewData["IdTdocuC"] = new SelectList(_context.TdocumentoCs, "IdTdocuC", "Detalle", chofere.IdTdocuC);
            return View(chofere);
        }

        // GET: Choferes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofere = await _context.Choferes
                .Include(c => c.IdEstadoNavigation)
                .Include(c => c.IdProvinciaNavigation)
                .Include(c => c.IdTdocuCNavigation)
                .FirstOrDefaultAsync(m => m.IdChofer == id);
            if (chofere == null)
            {
                return NotFound();
            }

            return View(chofere);
        }

        // POST: Choferes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Choferes == null)
            {
                return Problem("Entity set 'TAIProdContext.Choferes'  is null.");
            }
            var chofere = await _context.Choferes.FindAsync(id);
            if (chofere != null)
            {
                _context.Choferes.Remove(chofere);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChofereExists(int id)
        {
          return _context.Choferes.Any(e => e.IdChofer == id);
        }
    }
}
