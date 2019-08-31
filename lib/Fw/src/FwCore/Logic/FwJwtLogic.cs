using FwStandard.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FwCore.Logic
{
    public static class FwJwtLogic
    {
        //---------------------------------------------------------------------------------------------
        public static JwtSecurityToken CreateJwtSecurityToken(FwApplicationConfig _appConfig, IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                    issuer:             _appConfig.JwtIssuerOptions.Issuer,
                    audience:           _appConfig.JwtIssuerOptions.Audience,
                    claims:             claims,
                    notBefore:          _appConfig.JwtIssuerOptions.NotBefore,
                    expires:            _appConfig.JwtIssuerOptions.Expiration,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfig.JwtIssuerOptions.SecretKey)), SecurityAlgorithms.HmacSha256) );

            return jwt;
        }
        //---------------------------------------------------------------------------------------------
        public static string GenerateEncodedJwtToken(FwApplicationConfig _appConfig, IEnumerable<Claim> claims)
        {
            var jwt = CreateJwtSecurityToken(_appConfig, claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        //---------------------------------------------------------------------------------------------
    }
}
