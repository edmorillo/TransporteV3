using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TransporteV3.Entidades
{
    public partial class Compra
    {
        public int IdCompra { get; set; }
        public int? Cantidad { get; set; }
        [StringLength(maximumLength: 180, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son {1} caracteres")]
        public string Detalle { get; set; }
        public decimal? Precio { get; set; }
        public int? IdFormaPago { get; set; }
        [Display(Name = "Fecha de compra")]
        [DataType(DataType.Date)]
        public DateTime? FechaCompra { get; set; }
        [Display(Name = "Fecha de Pago")]
        public virtual FormasPago IdFormaPagoNavigation { get; set; }
    }
}
