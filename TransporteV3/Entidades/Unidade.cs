using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class Unidade
    {
        public Unidade()
        {
            LicenciasUnidads = new HashSet<LicenciasUnidad>();
        }

        public int IdUnidad { get; set; }
        public string Matricula { get; set; }
        public string Chasis { get; set; }
        
        public string Modelo { get; set; }
        [DataType(DataType.Date)]
        
        public DateTime? Año { get; set; }
        [Display(Name = "Capacidad de Carga")]
        public string CapacidadCarga { get; set; }
        [Display(Name = "Tipo de Unidad")]
        public int? IdTipoUnidad { get; set; }
        [Display(Name = "Neumatico")]
        public int? IdNeumatico { get; set; }
        public int? Kilometros { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Mantenimiento")]
        public DateTime? FechaMantenimiento { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Compra")]
        public DateTime? FechaCompra { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Vencimiento de la Unidad")]
        public DateTime? VencimientoUnidad { get; set; }
        [Display(Name = "Neumatico")]
        public virtual Neumatico IdNeumaticoNavigation { get; set; }
        [Display(Name = "Tipo de Unidad")]
        public virtual TipoUnidade IdTipoUnidadNavigation { get; set; }
        public virtual ICollection<LicenciasUnidad> LicenciasUnidads { get; set; }
    }
}
