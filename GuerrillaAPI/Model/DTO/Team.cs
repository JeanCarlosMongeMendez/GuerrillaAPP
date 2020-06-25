using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPI.Model.DTO
{
    public class Team
    {
        public string GUERRILLA { get; set; }
        public int ASSAULT { get; set; }
        public int ENGINEER { get; set; }
        public int TANK { get; set; }
        public int BUNKER { get; set; }
        public int OFFENSE { get; set; }
        public int DEFENSE { get; set; }
        public float DI { get; set; }
        public float AI { get; set; }
        public float LOOT_CAP { get; set; }
        public losses LOSSES { get; set; }
        public loot LOOT { get; set; }
    }
}
