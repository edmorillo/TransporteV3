using Microsoft.EntityFrameworkCore;
using TransporteV3.Entidades;

namespace TransporteV3.Servicios
{
    public class DatosExistentes
    {
        private readonly TAIProdContext _context;
        public async Task<bool> Existe(string nombre, int idchofer)
        {
            var existe = await _context.Choferes
                .AnyAsync(tc => tc.Nombre == nombre && tc.IdChofer == idchofer);

            return existe;
        }
    }
}
