
using Core.Dtos;
using Core.Entities;
using Core.Extentions;
using Core.Utilities.Encryption;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private IConfiguration Configuration;
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaimDto> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.Expiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecretKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwtSecurityToken = CreatejwtSecurityToken(user, operationClaims, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new AccessToken() { Token = token, Expiration = _accessTokenExpiration };
        }

        private JwtSecurityToken CreatejwtSecurityToken(User user, List<OperationClaimDto> operationClaims, Microsoft.IdentityModel.Tokens.SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: CreateClaims(user, operationClaims),
                notBefore: DateTime.Now,
                expires: _accessTokenExpiration,
                signingCredentials: signingCredentials);
        }

        private IEnumerable<Claim> CreateClaims(User user, List<OperationClaimDto> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddName(user.FirstName + " " + user.LastName);
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims);

            return claims;
        }
    }
}
