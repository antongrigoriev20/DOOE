using System;
using System.Collections.Generic;
using System.Text;

namespace OdesaoblenergoLib.Models.Responses
{
    public class AddIndicatorsResponse
    {
        public AddIndicators response { get; set; }
    }
    public class AddIndicators
    {
        public int errorno { get; set; }
        public string r { get; set; }
        public string fer { get; set; }
    }
}
