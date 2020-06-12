using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPP.Domain
{
    public class Guerrilla
    {
        public Guerrilla(int idGuerrilla,String nombreGuerrilla,String correoGuerrilla) {
            this.idGuerrilla = idGuerrilla;
            this.nombreGuerrilla = nombreGuerrilla;
            this.correoGuerrilla = correoGuerrilla;
        }
        public int idGuerrilla { get; set; }
        public String nombreGuerrilla { get; set; }
        public String correoGuerrilla { get; set; }
        public ArrayList unidadesDeBatalla { get; set; }
        public ArrayList listaRecursos { get; set; }
    }
}
