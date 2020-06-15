using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GuerrillaAPI.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace GuerrillaAPP.Controllers
{
    [Route("[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class AtaqueController : ControllerBase
    {
        private readonly GuerillaAppJLDContext _context;
        public AtaqueController() {
            unidadesDeBatalla = new List<UnidadDeBatalla>();
            unidadesDeDefensa = new List<UnidadDeBatalla>();
        }
    public List<UnidadDeBatalla> unidadesDeBatalla { get; set; }
    public List<UnidadDeBatalla> unidadesDeDefensa { get; set; }

     [HttpPost("{guerrillaName}")]
     public void ataque(String guerrillaName,String guerrillaSrc)
        {
            Guerrilla guerrillaAtacada = _context.Guerrilla.Where(g => g.guerrillaName.Equals(guerrillaName)).Single();
            Guerrilla guerrillaAtacante = _context.Guerrilla.Where(g => g.guerrillaName.Equals(guerrillaSrc)).Single();

            this.unidadesDeBatalla = guerrillaAtacante.army;
            this.unidadesDeDefensa = guerrillaAtacada.army;
            float os=calcularOS();
            float ds = calcularDS();
            float ai = (os / (ds + os))+ (float)0.10;
            float di = (ds / (ds + os)) + (float)0.10;
            int contadorUnidadDefensa=0;
            foreach (UnidadDeBatalla unidadDeBatalla in unidadesDeBatalla)
            {
                UnidadDeBatalla unidadDeDefensaActual = (UnidadDeBatalla)this.unidadesDeDefensa.ElementAt(contadorUnidadDefensa);
                while (unidadDeBatalla.defensa>0 && unidadDeDefensaActual.defensa>0)
                {
                    unidadDeDefensaActual.defensa= (float)unidadDeDefensaActual.defensa-(unidadDeBatalla.ataque+ai);
                    unidadDeDefensaActual.defensa = (float)Math.Floor(unidadDeDefensaActual.defensa);

                    unidadDeBatalla.defensa = (float)unidadDeBatalla.defensa - (unidadDeDefensaActual.defensa+di);
                    unidadDeBatalla.defensa = (float)Math.Floor(unidadDeBatalla.defensa);
                }
                contadorUnidadDefensa += 1;
            }
            
        }
    private float calcularOS() {
            float total = 0;
            foreach (UnidadDeBatalla unidadDeBatalla in this.unidadesDeBatalla)
            {
                total += unidadDeBatalla.ataque;
            }
            return total;
    }
    private float calcularDS()
        {
            float total = 0;
            foreach (UnidadDeBatalla unidadDeDefensa in this.unidadesDeDefensa)
            {
                total += unidadDeDefensa.defensa;
            }
            return total;
        }
    
     
    }
}
