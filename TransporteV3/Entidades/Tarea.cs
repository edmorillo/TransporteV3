using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public class tarea
    {
        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacionId { get; set; }
    }
}
