using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class TiposDocumento
    {
        public TiposDocumento()
        {
            LicenciaChofers = new HashSet<LicenciaChofer>();
            LicenciasUnidads = new HashSet<LicenciasUnidad>();
        }

        public int IdTiposDocumentos { get; set; }
        [Display(Name = "Tipo de Licencia")]
        public string TipoDocumento { get; set; }

        public virtual ICollection<LicenciaChofer> LicenciaChofers { get; set; }
        public virtual ICollection<LicenciasUnidad> LicenciasUnidads { get; set; }
    }
}
