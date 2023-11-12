using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class TipoUnidade
    {
        public TipoUnidade()
        {
            Unidades = new HashSet<Unidade>();
        }

        public int IdTipoUnidad { get; set; }
        [StringLength(maximumLength: 180, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son {1} caracteres")]
        public string Detalle { get; set; }
        [Display(Name = "Tipo de Marca de las unidades")]
        public int? IdTipoMarcaUnidades { get; set; }
        [Display(Name = "Tipo de Marca de las unidades")]
        public virtual TipoMarcasUnidade IdTipoMarcaUnidadesNavigation { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
    }
}
