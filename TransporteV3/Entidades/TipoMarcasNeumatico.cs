using System;
using System.Collections.Generic;

namespace TransporteV3.Entidades
{
    public partial class TipoMarcasNeumatico
    {
        public TipoMarcasNeumatico()
        {
            Neumaticos = new HashSet<Neumatico>();
        }

        public int IdTipoMarcaNeumaticos { get; set; }
        public string? TipoMarcaNeumatico { get; set; }

        public virtual ICollection<Neumatico> Neumaticos { get; set; }
    }
}
