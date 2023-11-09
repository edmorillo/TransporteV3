using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TransporteV3.Entidades
{
    public partial class LicenciaChofer
    {
        public int IdLicenciaChofer { get; set; }
        [Display(Name = "Chofer")]
        public int? IdChofer { get; set; }
        [Display(Name = "Tipos Licencias")]
        public int? IdTiposDocumentos { get; set; }
        [Display(Name = "Fecha Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }
        [Display(Name = "Chofer")]
        public virtual Chofere IdChoferNavigation { get; set; }
        [Display(Name = "Tipos Licencias")]
        public virtual TiposDocumento IdTiposDocumentosNavigation { get; set; }
    }
}
