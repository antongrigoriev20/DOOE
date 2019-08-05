using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Requests
{
    public class TokenRequestParams
    {
        [JsonProperty(PropertyName = "secret_key")]
        public string Secret_key { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
