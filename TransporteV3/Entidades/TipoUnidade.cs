using System;
using System.Collections.Generic;

namespace TransporteV3.Entidades
{
    public partial class TipoUnidade
    {
        public TipoUnidade()
        {
            Unidades = new HashSet<Unidade>();
        }

        public int IdTipoUnidad { get; set; }
        public string? Detalle { get; set; }
        public int? IdTipoMarcaUnidades { get; set; }

        public virtual TipoMarcasUnidade? IdTipoMarcaUnidadesNavigation { get; set; }
        public virtual ICollection<Unidade> Unidades { get; set; }
    }
}
