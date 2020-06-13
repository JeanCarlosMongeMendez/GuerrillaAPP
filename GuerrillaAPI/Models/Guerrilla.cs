using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Models
{
    public partial class Guerrilla
    {
        public int IdGuerrilla { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string TipoGuerrilla { get; set; }

        public virtual RecursoDeGuerrilla RecursoDeGuerrilla { get; set; }
        public virtual UnidadesDeGuerrilla UnidadesDeGuerrilla { get; set; }
    }
}
