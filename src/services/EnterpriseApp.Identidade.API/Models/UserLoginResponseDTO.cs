namespace EnterpriseApp.Identidade.API.Models
{
    public class UserLoginResponseDTO
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenDTO UserToken { get; set; }
    }
}
