using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class FormasPago
    {
        public FormasPago()
        {
            Compras = new HashSet<Compra>();
            Viajes = new HashSet<Viaje>();
        }

        public int IdFormaPago { get; set; }
        [Display(Name = "Forma de Pago")]
        public string FormaPago { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }
    }
}
