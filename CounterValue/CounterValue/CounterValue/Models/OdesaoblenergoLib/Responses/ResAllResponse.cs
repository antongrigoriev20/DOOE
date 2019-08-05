using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Responses
{
    public class ResAllResponse
    {
        public List<Res> response { get; set; }
    }
    public class Res
    {
        public string OfficeId { get; set; }
        public string OfficeNameUa { get; set; }
        public string errorno { get; set; }
    }
}
