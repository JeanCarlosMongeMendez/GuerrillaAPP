using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class UnidadesDeGuerrilla
    {
        public int IdGuerrilla { get; set; }
        public int? IdUnidad { get; set; }

        public virtual Guerrilla IdGuerrillaNavigation { get; set; }
        public virtual UnidadesDeBatalla IdUnidadNavigation { get; set; }
    }
}
