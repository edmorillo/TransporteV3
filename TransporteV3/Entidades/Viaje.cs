using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class Viaje
    {
        public int IdViajes { get; set; }
        public string Viajes { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        [Display(Name = "Chofer")]
        public int? IdChofer { get; set; }
        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }
        public decimal? Tarifa { get; set; }
        [Display(Name = "Forma de Pago")]
        public int? IdFormaPago { get; set; }
        [Display(Name = "Es cobrado")]
        [StringLength(maximumLength: 19, MinimumLength = 0, ErrorMessage = "La logintud del campo debe contener {1} digitos")]
        public string Escobrado { get; set; }
        public string Detalle { get; set; }
        public string Remito { get; set; }
        [Display(Name = "N° contenedor")]
        public int? Ncontenedor { get; set; }
        [Display(Name = "Es Facturado")]
        [StringLength(maximumLength: 19, MinimumLength = 3, ErrorMessage = "La logintud del campo {0} debe contener {1} digitos")]
        public string EsFacturado { get; set; }
        public string Entidad { get; set; }
        [Display(Name = "N° factura")]
        public string Nfactura { get; set; }
        [Display(Name = "Chofer")]
        public virtual Chofere IdChoferNavigation { get; set; }
        [Display(Name = "Cliente")]
        public virtual Cliente IdClienteNavigation { get; set; }
        [Display(Name = "Forma de Pago")]
        public virtual FormasPago IdFormaPagoNavigation { get; set; }
    }
}
