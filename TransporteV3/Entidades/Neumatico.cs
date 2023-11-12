using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class Neumatico
    {
        public Neumatico()
        {
            Unidades = new HashSet<Unidade>();
        }

        public int IdNeumatico { get; set; }
        public string Marca { get; set; }
        public int? Rodado { get; set; }
        public int? Modelo { get; set; }
        public int? Kilometraje { get; set; }
        [Display(Name = "Tipo de Marca")]
        public int? IdTipoMarcaNeumaticos { get; set; }
        [Display(Name = "Tipo de Marca")]
        public virtual TipoMarcasNeumatico IdTipoMarcaNeumaticosNavigation { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
    }
}
