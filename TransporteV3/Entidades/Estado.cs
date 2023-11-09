using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TransporteV3.Entidades
{
    public partial class Estado
    {
        public Estado()
        {
            Choferes = new HashSet<Chofere>();
        }

        public int IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado1 { get; set; }

        public virtual ICollection<Chofere> Choferes { get; set; }
    }
}
