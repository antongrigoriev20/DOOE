using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Requests
{
    public class AbonentInfoRequestParams
    {
        public string token { get; set; }
        public string res { get; set; }
        public string lsch { get; set; }
        public string abonent { get; set; }
        public string proctype { get; set; }
        public string phoneno { get; set; }
    }
}
