using OdesaoblenergoLib.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterValue.Models
{
    public class User
    {
        public SelectIndicatorsResponse IndicatorsResponse { get; set; }
        public AbonentInfoResponse AbonentInfo { get; set; }
        public string LastData { get; set; }
        public string PenultimateData { get; set; } 
    }
}
