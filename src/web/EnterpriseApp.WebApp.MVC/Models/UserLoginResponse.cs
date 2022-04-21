using EnterpriseApp.Core.Communication;
using System;
using System.Collections.Generic;

namespace EnterpriseApp.WebApp.MVC.Models
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }
        public UserTokenDTO UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UserTokenDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimDTO> Claims { get; set; }
    }

    public class UserClaimDTO
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
