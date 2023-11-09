using System;
using System.Collections.Generic;

namespace TransporteV3.Entidades
{
    public partial class Provincium
    {
        public Provincium()
        {
            Choferes = new HashSet<Chofere>();
            Clientes = new HashSet<Cliente>();
            Localidades = new HashSet<Localidade>();
        }

        public int IdProvincia { get; set; }
        public string? Provincia { get; set; }

        public virtual ICollection<Chofere> Choferes { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Localidade> Localidades { get; set; }
    }
}
