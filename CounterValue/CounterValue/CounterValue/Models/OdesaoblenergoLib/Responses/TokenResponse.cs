using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdesaoblenergoLib.Models.Responses
{
    public class TokenResponse
    {
        public Token Response { get; set; }
        public string Error { get; set; } //???!!!
    }
    public class Token
    {
        public string Access_token { get; set; }
    }
}
