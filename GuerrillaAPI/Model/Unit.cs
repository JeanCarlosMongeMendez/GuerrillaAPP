using System;
using System.Collections.Generic;

namespace GuerrillaAPI.Model
{
    public partial class Unit
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int People { get; set; }
        public int Oil { get; set; }
        public int Loot { get; set; }
        public int Offense { get; set; }
        public int Defense { get; set; }
        public double Assault { get; set; }
        public double Engineer { get; set; }
        public double Tank { get; set; }
        public double Bunker { get; set; }
    }
}
