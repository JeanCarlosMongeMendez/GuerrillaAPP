using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class UnidadesDeBatalla
    {
        public UnidadesDeBatalla()
        {
            UnidadesDeGuerrilla = new HashSet<UnidadesDeGuerrilla>();
        }

        public int IdUnidad { get; set; }
        public string NombreUnidad { get; set; }
        public decimal? Ataque { get; set; }
        public decimal? Pillaje { get; set; }
        public decimal? Defensa { get; set; }
        public decimal CostoDinero { get; set; }
        public decimal CostoPetroleo { get; set; }
        public decimal CostoUnidades { get; set; }

        public virtual ICollection<UnidadesDeGuerrilla> UnidadesDeGuerrilla { get; set; }
    }
}
