using System;
using System.Collections.Generic;

namespace TransporteV3.Entidades
{
    public partial class TdocumentoC
    {
        public TdocumentoC()
        {
            Choferes = new HashSet<Chofere>();
        }

        public int IdTdocuC { get; set; }
        public string? Detalle { get; set; }

        public virtual ICollection<Chofere> Choferes { get; set; }
    }
}
