using FwCore.AppManager;
using FwCore.Logic;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Modules.AccountServices.Jwt;

namespace FwCore.Controllers
{
    public class FwJwtController : Controller
    {
        protected readonly FwApplicationConfig _appConfig;
        private readonly ILogger _logger;
        //private readonly JsonSerializerSettings _serializerSettings;
        //---------------------------------------------------------------------------------------------
        public FwJwtController(IOptions<FwApplicationConfig> appConfig, ILoggerFactory loggerFactory)
        {
            _appConfig = appConfig.Value;
            ThrowIfInvalidOptions(_appConfig.JwtIssuerOptions);

            _logger = loggerFactory.CreateLogger<FwJwtController>();

            //_serializerSettings = new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented
            //};
        }
        //---------------------------------------------------------------------------------------------
        public class JwtResponseModel
        {
            public int statuscode { get; set; }
            public string statusmessage { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public bool resetpassword { get; set; } = false;
        }
        //---------------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<JwtResponseModel>> DoPost([FromBody] FwStandard.Models.FwApplicationUser user)
        {
            JwtResponseModel response = new JwtResponseModel();

            dynamic userauth = await FwSqlData.UserAuthenticationAsync(_appConfig.DatabaseSettings, user.UserName, user.Password);

            if (userauth.Status == 0)
            {
                ClaimsIdentity identity = await FwAmUserClaimsProvider.GetClaimsIdentity(_appConfig.DatabaseSettings, userauth.WebUsersId);
                var jwtClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, await _appConfig.JwtIssuerOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_appConfig.JwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
                };
                List<Claim> claims = new List<Claim>();
                claims.AddRange(jwtClaims);
                claims.AddRange(identity.Claims);

                // Create the JWT security token and encode it.
                var encodedJwt = FwJwtLogic.GenerateEncodedJwtToken(_appConfig, claims);

                // Serialize and return the response
                response.statuscode   = 0;
                response.access_token = encodedJwt;
                response.expires_in   = (int)_appConfig.JwtIssuerOptions.ValidFor.TotalSeconds;

                //Check if user needs to reset their password
                response.resetpassword = await FwSqlData.CheckPasswordExpirationAsync(_appConfig.DatabaseSettings, userauth.WebUsersId);
            }
            else if (userauth.Status == 100) //Failed Basic Authentication
            {
                response.statuscode    = 401; //Unauthorized
                response.statusmessage = "Invalid user and/or password.";
            }

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(response);
        }
        //---------------------------------------------------------------------------------------------
        public class IntegrationResponseModel
        {
            public int statuscode { get; set; }
            public string statusmessage { get; set; }
            public string access_token { get;set; }
            public string expires_in { get; set; }
            public string dealid { get; set; }
            public string campusid { get; set; }

        }
        //---------------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<IntegrationResponseModel>> DoIntegrationPost([FromBody] FwStandard.Models.FwIntegration client)
        {
            dynamic response = new ExpandoObject();
            var identity = await FwAmUserClaimsProvider.GetIntegrationClaimsIdentity(_appConfig.DatabaseSettings, client.client_id, client.client_secret);
            if (identity == null)
            {
                response.statuscode    = 401; //Unauthorized
                response.statusmessage = "Invalid client ID and/or secret key."; 
            }
            else
            {
                var jwtClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, client.client_id),
                    new Claim(JwtRegisteredClaimNames.Jti, await _appConfig.JwtIssuerOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_appConfig.JwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
                };
                List<Claim> claims = new List<Claim>();
                claims.AddRange(jwtClaims);
                claims.AddRange(identity.Claims);

                // Create the JWT security token and encode it.
                var encodedJwt = FwJwtLogic.GenerateEncodedJwtToken(_appConfig, claims);

                // Serialize and return the response
                response.statuscode   = 0;
                response.access_token = encodedJwt;
                response.expires_in   = (int)_appConfig.JwtIssuerOptions.ValidFor.TotalSeconds;
                response.dealid       = identity.FindFirst(AuthenticationClaimsTypes.DealId).Value;
                response.campusid     = identity.FindFirst(AuthenticationClaimsTypes.CampusId).Value;
            }

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(response);
        }
        //---------------------------------------------------------------------------------------------
        protected virtual async Task<ActionResult<JwtResponseModel>> DoOktaPost([FromBody] OktaRequest request)
        {
            dynamic response = new ExpandoObject();
            var identity = await FwJwtLogic.GetOktaClaimsIdentity(this._appConfig.DatabaseSettings, request);
            if (identity == null)
            {
                response.statuscode = 401; //Unauthorized
                response.statusmessage = "Invalid user and/or password.";
            }
            else
            {
                var jwtClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, await _appConfig.JwtIssuerOptions.JtiGenerator()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_appConfig.JwtIssuerOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64)
                };
                List<Claim> claims = new List<Claim>();
                claims.AddRange(jwtClaims);
                claims.AddRange(identity.Claims);

                // Create the JWT security token and encode it.
                var encodedJwt = FwJwtLogic.GenerateEncodedJwtToken(_appConfig, claims);

                // Serialize and return the response
                response.statuscode = 0;
                response.access_token = encodedJwt;
                response.expires_in = (int)_appConfig.JwtIssuerOptions.ValidFor.TotalSeconds;
            }

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(response);
        }
        //----------------------------------------------------------------------------------------------
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
        protected static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        //---------------------------------------------------------------------------------------------
    }
}