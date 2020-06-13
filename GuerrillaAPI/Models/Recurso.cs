using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class Recurso
    {
        public int IdRecurso { get; set; }
        public string Nombre { get; set; }

        public virtual RecursoDeGuerrilla RecursoDeGuerrilla { get; set; }
    }
}
