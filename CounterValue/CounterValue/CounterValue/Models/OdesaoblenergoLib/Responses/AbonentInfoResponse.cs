using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Responses
{
    public class AbonentInfoResponse
    {
        public AbonentInfo response { get; set; }
    }
    public class AbonentInfo
    {
        public string AbonentId { get; set; }
        public string lsch { get; set; }
        public string Email { get; set; }
        public string sendflag { get; set; }
        public string abonaddr { get; set; }
        public string eladdr { get; set; }
        public string catname { get; set; }
        public string Passport { get; set; }
        public string AbonentTaxCode { get; set; }
    }
}
