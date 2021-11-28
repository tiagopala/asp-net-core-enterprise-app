using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseApp.Identidade.API.Models
{
    public class UserLoginResponseDTO
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDTO UserToken { get; set; }
    }
}
