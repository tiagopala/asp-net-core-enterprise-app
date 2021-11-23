using EnterpriseApp.Identidade.API.Configurations;
using EnterpriseApp.Identidade.API.Extensions;
using EnterpriseApp.Identidade.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<JwtConfig> jwtConfig)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateErrors();
                return BadRequest(new { Errors = errors });
            }

            var identityUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = result.GetIdentityErrors();
                return BadRequest(new { Errors = errors });
            }

            await _signInManager.SignInAsync(identityUser, false);
            return Ok(await GenerateToken(user.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateErrors();
                return BadRequest(new { Errors = errors });
            }

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

            if (!result.Succeeded) return BadRequest();

            return Ok(await GenerateToken(user.Email));
        }

        private async Task<UserLoginResponseDTO> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            ClaimsIdentity identityClaims = new();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return new UserLoginResponseDTO
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_jwtConfig.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c =>
                    new UserClaimDTO
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
