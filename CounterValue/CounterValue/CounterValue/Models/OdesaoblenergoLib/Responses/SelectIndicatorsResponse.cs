using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Responses
{
    public class SelectIndicatorsResponse
    {
        public SelectIndicators response { get; set; }
    }

    public class SelectIndicators
    {
        public string errorno { get; set; }
        public string digits { get; set; }
        public string Zones { get; set; }
        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }
        public string CntName { get; set; }
        public string zn1 { get; set; }
        public string zn2 { get; set; }
        public string zn3 { get; set; }
        public string number { get; set; }
    }
}
