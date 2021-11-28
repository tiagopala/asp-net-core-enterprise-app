﻿using EnterpriseApp.Identidade.API.Configurations;
using EnterpriseApp.Identidade.API.Extensions;
using EnterpriseApp.Identidade.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Identidade.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
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
            return new StatusCodeResult(401);

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var identityUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (!result.Succeeded)
                return CustomResponse(result);    

            return CustomResponse(await GenerateToken(user.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO user)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, true);

            if (!result.Succeeded)
            {
                AddError(result.GetSignInError());
                return CustomResponse();
            }

            return CustomResponse(await GenerateToken(user.Email));
        }

        private async Task<UserLoginResponseDTO> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(claims, user);
            var encodedToken = GetToken(identityClaims);

            return GetUserLoginResponse(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
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

            return identityClaims;
        }

        private string GetToken(ClaimsIdentity identityClaims)
        {
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

            return tokenHandler.WriteToken(token);
        }

        private UserLoginResponseDTO GetUserLoginResponse(string encodedToken, IdentityUser user, IList<Claim> claims)
        {
            return new UserLoginResponseDTO
            {
                AccessToken = encodedToken,
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
