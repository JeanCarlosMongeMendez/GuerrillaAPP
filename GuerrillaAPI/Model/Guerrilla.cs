using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Model
{
    public partial class Guerrilla
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Faction { get; set; }
        public int Timestamp { get; set; }
        public int Rank { get; set; }
        public bool Available { get; set; }
    }
}
