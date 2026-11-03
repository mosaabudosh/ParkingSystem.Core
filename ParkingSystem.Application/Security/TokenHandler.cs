

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ParkingSystem.Application.Security
{
    public class TokenHandler : ITokenHandler
    {
        #region Fields
        private readonly TokenOptions _tokenOptions;
        private readonly SigningCredentialsConfiguration _signingConfigurations;
        private readonly IConfiguration _configuration;
        #endregion

        public TokenHandler(IOptions<TokenOptions> tokenOptions, SigningCredentialsConfiguration signingConfigurations, IConfiguration configuration)
        {
            _tokenOptions = tokenOptions.Value;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }
        public User BuildUserTokens(User user)
        {
            try
            {
                int refreshTokenExpirationMinutes = 5;
                int accessTokenValidDays = 1;
                int tokenSize = 256;

                if (_configuration.GetSection("AuthenticationOption").Exists())
                {
                    refreshTokenExpirationMinutes = _configuration.GetSection("AuthenticationOption").GetSection("RefreshTokenExpirationMinutes").Exists() ? Convert.ToInt32(_configuration.GetSection("AuthenticationOption").GetSection("RefreshTokenExpirationMinutes").Value) : refreshTokenExpirationMinutes;
                    accessTokenValidDays = _configuration.GetSection("AuthenticationOption").GetSection("AccessTokenValidDays").Exists() ? Convert.ToInt32(_configuration.GetSection("AuthenticationOption").GetSection("AccessTokenValidDays").Value) : accessTokenValidDays;
                    tokenSize = _configuration.GetSection("AuthenticationOption").GetSection("RefreshTokenSizeByt").Exists() ? Convert.ToInt32(_configuration.GetSection("AuthenticationOption").GetSection("RefreshTokenSizeByt").Value) : tokenSize;
                }
                user.AccessToken = GenerateToken(user, DateTime.Now.AddDays(accessTokenValidDays));
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
        private string GenerateToken(User? user, DateTime tokenExpiration, bool isRefresh = false)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                if (isRefresh)
                {
                    claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, tokenExpiration.ToString())
                    };
                }
                else
                {
                    if (user is not null)
                    {
                        claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Name, $"{user.FullName}"),
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),

                        };
                        if (user.UserRoles != null && user.UserRoles.Any(p => p.Role != null))
                        {
                            claims.AddRange(user.UserRoles.Where(p => p.Role != null).Select(s => new Claim(ClaimTypes.Role, s.Role.Name)).ToList());
                        }
                    }
                  
                }
                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = tokenExpiration,
                    IssuedAt = DateTime.Now,
                    Subject = new ClaimsIdentity(claims),
                    Issuer = _tokenOptions.Issuer,
                    Audience = _tokenOptions.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials
                };
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                return jwtSecurityTokenHandler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
