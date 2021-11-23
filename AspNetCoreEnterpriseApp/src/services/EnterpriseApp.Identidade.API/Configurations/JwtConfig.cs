using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Identidade.API.Configurations
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
