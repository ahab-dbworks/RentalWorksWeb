using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RentalWorksWebApi;
using RentalWorksWebApi.Data;
using RentalWorksWebApi.Models;
using RentalWorksWebApi.Options;
using RentalWorksWebApi.Security;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RentalWorksApi.Controllers
{
    [Route("api/v1/jwt")]
    public class JwtController : Controller
    {
        private readonly ApplicationConfig _appConfig;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;
        //---------------------------------------------------------------------------------------------
        public JwtController(IOptions<ApplicationConfig> appConfig, IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory)
        {
            _appConfig = appConfig.Value;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<JwtController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]ApplicationUser user)
        {
            dynamic response = new ExpandoObject();
            var identity = await UserClaimsProvider.GetClaimsIdentity(_appConfig.DatabaseSettings, user.UserName, user.Password);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({user.UserName}) or password ({user.Password})"); //MY 10/12/2017: Not sure we should log failed attempts with user entered info. Could be harvested to guess correct passwords.
                //return BadRequest("Invalid credentials"); //MY 10/12/2017: Removed so we can control the error on front end.
                response.statuscode    = 401; //Unauthorized
                response.statusmessage = "Invalid user and/or password.";
            }
            else
            {
                var jwtClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                };
                List<Claim> claims = new List<Claim>();
                claims.AddRange(jwtClaims);
                claims.AddRange(identity.Claims);
                

                // Create the JWT security token and encode it.
                var jwt = new JwtSecurityToken(
                    issuer:             _jwtOptions.Issuer,
                    audience:           _jwtOptions.Audience,
                    claims:             claims,
                    notBefore:          _jwtOptions.NotBefore,
                    expires:            _jwtOptions.Expiration,
                    signingCredentials: _jwtOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                // Serialize and return the response
                response.statuscode   = 0;
                response.access_token = encodedJwt;
                response.expires_in   = (int)_jwtOptions.ValidFor.TotalSeconds;
            }

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }
        //---------------------------------------------------------------------------------------------
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
        //---------------------------------------------------------------------------------------------
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        //---------------------------------------------------------------------------------------------
    }
}