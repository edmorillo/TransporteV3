using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TransporteV3.Entidades
{
    public partial class CondicionIva
    {
        public CondicionIva()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int IdCondicionIva { get; set; }
        [Display(Name = "Condicción IVA")]
        public string CondicionIva1 { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
