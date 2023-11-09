using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransporteV3.Entidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            Viajes = new HashSet<Viaje>();
        }

        public int IdCliente { get; set; }
        [StringLength(maximumLength: 49, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son 50 caracteres")]
        [RegularExpression("[a-z A-Z]{0,49}", ErrorMessage = "Solo ingrese texto (A-z)")]
        public string Nombre { get; set; }
        [StringLength(maximumLength: 49, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son 50 caracteres")]
        [RegularExpression("[a-z A-Z]{0,49}", ErrorMessage = "Solo ingrese texto (A-z)")]
        public string Apellido { get; set; }
        [StringLength(maximumLength: 49, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son 50 caracteres")]
        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "La logintud del campo {0} debe contener {1} digitos")]
        [RegularExpression("[0-9]{11,11}", ErrorMessage = "En el campo {0} solo ingrese números")]
        public string Cuit { get; set; }
        [Display(Name = "Provincia")]
        public int IdProvincia { get; set; }
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "La logintud máxima del campo son 150 caracteres")]
        public string Direccion { get; set; }
        
        
        public int CodigoPostal { get; set; }
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }
        
        public int Telefono { get; set; }
        [Display(Name = "Condicción IVA")]
        public int IdCondicionIva { get; set; }
        [Display(Name = "Ingreso Bruto")]
        public string IngresosBrutos { get; set; }

        [Display(Name = "Condicción IVA")]
        public virtual CondicionIva IdCondicionIvaNavigation { get; set; }
        [Display(Name = "Provincia")]
        public virtual Provincium IdProvinciaNavigation { get; set; }
        public virtual ICollection<Viaje> Viajes { get; set; }
    }
}
