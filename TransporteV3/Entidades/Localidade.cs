using System;
using System.Collections.Generic;

namespace TransporteV3.Entidades
{
    public partial class Localidade
    {
        public int IdLocalidad { get; set; }
        public int? IdProvincia { get; set; }
        public string? Localidad { get; set; }

        public virtual Provincium? IdProvinciaNavigation { get; set; }
    }
}
