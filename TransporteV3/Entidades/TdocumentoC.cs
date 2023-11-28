using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class TdocumentoC
    {
        public TdocumentoC()
        {
            Choferes = new HashSet<Chofere>();
        }

        public int IdTdocuC { get; set; }
        [Display(Name = "Tipo de documento")]
        public string Detalle { get; set; }

        public virtual ICollection<Chofere> Choferes { get; set; }
    }
}
