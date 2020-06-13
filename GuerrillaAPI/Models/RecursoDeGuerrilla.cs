using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class RecursoDeGuerrilla
    {
        public int IdGuerrilla { get; set; }
        public int IdRecurso { get; set; }

        public virtual Recurso IdGuerrilla1 { get; set; }
        public virtual Guerrilla IdGuerrillaNavigation { get; set; }
    }
}
