using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuerrillaAPI.Model.DTO
{
    public class attackResult
    {
        public List<guerrilla> guerrillas { get; set; }
        public List<result> results { get; set; }
    }
}
