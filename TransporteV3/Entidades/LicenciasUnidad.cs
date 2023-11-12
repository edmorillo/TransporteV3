using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace TransporteV3.Entidades
{
    public partial class LicenciasUnidad
    {
        public int IdLicenciaUnidades { get; set; }
        [Display(Name = "Unidad")]
        public int? IdUnidad { get; set; }
        [Display(Name = "Tipos Licencias")]
        public int? IdTiposDocumentos { get; set; }
        [Display(Name = "Fecha Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }
        [Display(Name = "Tipos Licencias")]

        public virtual TiposDocumento IdTiposDocumentosNavigation { get; set; }
        [Display(Name = "Unidad")]
        public virtual Unidade IdUnidadNavigation { get; set; }
    }
}
