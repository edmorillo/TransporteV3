using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class TipoMarcasUnidade
    {
        public TipoMarcasUnidade()
        {
            TipoUnidades = new HashSet<TipoUnidade>();
        }

        public int IdTipoMarcaUnidad { get; set; }
        [Display(Name = "Tipo de Marca Unidad")]
        public string TipoMarcaUnidad { get; set; }

        public virtual ICollection<TipoUnidade> TipoUnidades { get; set; }
    }
}
