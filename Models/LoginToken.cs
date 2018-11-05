using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Models
{
    public class LoginToken
    {
        public string Series { get; set; }

        public string Token { get; set; }

        public string Identity { get; set; }
    }
}
