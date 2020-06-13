using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPP.Domain
{
    public class UnidadesDeBatalla
    {
        public UnidadesDeBatalla(int idUnidad,int cantidadUnidad,String nombreUnidad,float pillaje,float ataque,
            float defensa,float costoDinero,float costoPetroleo,float costoUnidades)
        {
            this.idUnidad = idUnidad;
            this.cantidadUnidad = cantidadUnidad;
            this.nombreUnidad = nombreUnidad;
            this.pillaje = pillaje;
            this.ataque = ataque;
            this.defensa = defensa;
            this.costoDinero = costoDinero;
            this.costoPetroleo = costoPetroleo;
            this.costoUnidades = costoUnidades;
        }
        public int idUnidad { get; set; }
        public int cantidadUnidad { get; set; }
        public String nombreUnidad { get; set; }
        public float pillaje { get; set; }
        public float ataque { get; set; }
        public float defensa { get; set; }
        public float costoDinero { get; set; }
        public float costoPetroleo { get; set; }
        public float costoUnidades { get; set; }
    }
}
