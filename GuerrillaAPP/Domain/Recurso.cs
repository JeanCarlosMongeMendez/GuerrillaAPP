using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPP.Domain
{
    public class Recurso
    {
        public Recurso(int idRecurso,int cantidadRecurso,String nombreRecurso){
            this.idRecurso = idRecurso;
            this.cantidadRecurso = cantidadRecurso;
            this.nombreRecurso = nombreRecurso;

        }
        public int idRecurso { get; set; }
        public int cantidadRecurso { get; set; }
        public String nombreRecurso { get; set; }
    }

}
