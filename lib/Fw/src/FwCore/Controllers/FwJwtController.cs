using FwCore.Security;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FwCore.Controllers
{
    public class FwJwtController : Controller
    {
        private readonly FwApplicationConfig _appConfig;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        //---------------------------------------------------------------------------------------------
        public FwJwtController(IOptions<FwApplicationConfig> appConfig, ILoggerFactory loggerFactory)
        {
            _appConfig = appConfig.Value;
            ThrowIfInvalidOptions(_appConfig.JwtIssuerOptions);

            _logger = loggerFactory.CreateLogger<FwJwtController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        //---------------------------------------------------------------------------------------------
        protected virtual async Task<IActionResult> DoPost([FromBody] FwStandard.Models.FwApplicationUser user)
        {
            dynamic response = new ExpandoObject();
            var identity = await UserClaimsProvider.GetClaimsIdentity(_appConfig.DatabaseSettings, user.UserName, user.Password);
            if (identity == null)
            {
                response.statuscode    = 401; //Unauthorized
                response.statusmessage = "Invalid user and/or password.";
            }
            else
            {
                var jwtClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, await _appConfig.JwtIssuerOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_appConfig.JwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                };
                List<Claim> claims = new List<Claim>();
                claims.AddRange(jwtClaims);
                claims.AddRange(identity.Claims);
                

                // Create the JWT security token and encode it.
                var jwt = new JwtSecurityToken(
                    issuer:             _appConfig.JwtIssuerOptions.Issuer,
                    audience:           _appConfig.JwtIssuerOptions.Audience,
                    claims:             claims,
                    notBefore:          _appConfig.JwtIssuerOptions.NotBefore,
                    expires:            _appConfig.JwtIssuerOptions.Expiration,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfig.JwtIssuerOptions.SecretKey)), SecurityAlgorithms.HmacSha256) );

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                // Serialize and return the response
                response.statuscode   = 0;
                response.access_token = encodedJwt;
                response.expires_in   = (int)_appConfig.JwtIssuerOptions.ValidFor.TotalSeconds;
            }

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
        //---------------------------------------------------------------------------------------------
        private static void ThrowIfInvalidOptions(FwJwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(FwJwtIssuerOptions.ValidFor));
            }

            if (string.IsNullOrEmpty(options.SecretKey))
            {
                throw new ArgumentNullException(nameof(FwJwtIssuerOptions.SecretKey));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(FwJwtIssuerOptions.JtiGenerator));
            }
        }
        //---------------------------------------------------------------------------------------------
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        //---------------------------------------------------------------------------------------------
    }
}