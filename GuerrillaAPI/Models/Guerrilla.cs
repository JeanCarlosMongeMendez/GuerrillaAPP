using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GuerrillaAPI.Models
{
    public partial class Guerrilla
    {
        public int IdGuerrilla { get; set; }
        public string guerrillaName { get; set; }
        public string email { get; set; }
        public string faction { get; set; }
        public int rank { get; set; }
        //public int timestamp { get; set; }
        public List<Recurso> resourses { get; set; }
        public List<UnidadesDeBatalla> buildings { get; set; }
        public List<UnidadesDeBatalla> army { get; set; }
    }
}
