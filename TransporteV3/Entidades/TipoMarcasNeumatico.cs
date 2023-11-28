using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class TipoMarcasNeumatico
    {
        public TipoMarcasNeumatico()
        {
            Neumaticos = new HashSet<Neumatico>();
        }

        public int IdTipoMarcaNeumaticos { get; set; }
        [Display(Name = "Tipo de Marca Neumático")]
        public string TipoMarcaNeumatico { get; set; }

        public virtual ICollection<Neumatico> Neumaticos { get; set; }
    }
}
