using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class UnidadDeBatalla
    {
        public int idUnidad { get; set; }
        public string nombreUnidad { get; set; }
        public float ataque { get; set; }
        public float pillaje { get; set; }
        public float defensa { get; set; }
        public float costoDinero { get; set; }
        public float costoPetroleo { get; set; }
        public float costoUnidades { get; set; }
    }
}
