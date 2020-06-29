using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPI.Model.DTO
{
    public class result
    {
        public string name { get; set; }
        public resources resources { get; set; }
        public buildings buildings { get; set; }
        public army army { get; set; }
    }
}
