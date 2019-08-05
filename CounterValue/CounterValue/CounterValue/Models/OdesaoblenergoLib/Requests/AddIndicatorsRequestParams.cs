using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Requests
{
    public class AddIndicatorsRequestParams
    {
        public string token { get; set; }
        public string res { get; set; }
        public string abonent { get; set; }
        public string date_count { get; set; }
        public string value_one_zone { get; set; }
        public string value_two_zone { get; set; }
        public string value_three_zone { get; set; }
        public string user_email { get; set; }
        public string email_send { get; set; }
    }
}
