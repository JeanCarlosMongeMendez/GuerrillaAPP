using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPI.Model.DTO
{
    public partial class guerrilla
    {
        public string guerrillaName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string faction { get; set; }
        public int timestamp { get; set; }
        public int rank { get; set; }
        public resources resources { get; set; }
        public buildings buildings { get; set; }
        public army army { get; set; }
    }
}
