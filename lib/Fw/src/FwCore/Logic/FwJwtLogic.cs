using FwCore.Api;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Modules.AccountServices.Jwt;

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
        public static HttpClient client = new HttpClient();
        public static async Task<ClaimsIdentity> GetOktaClaimsIdentity(SqlServerConfig dbConfig, OktaRequest request)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qryAuthenticate = new FwSqlCommand(conn, "webauthenticate", dbConfig.QueryTimeout))
                {
                    string webUsersId = await FwSqlCommand.GetStringDataAsync(conn, dbConfig.QueryTimeout, "webusersview", "email", request.Email, "webusersid");


                    if (!string.IsNullOrEmpty(webUsersId))
                    {
                        using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                        {
                            qry.Add("select top 1 wuv.*, groupsdatestamp = g.datestamp");
                            qry.Add("from webusersview wuv with (nolock)");
                            qry.Add("  join groups g  with (nolock) on (wuv.groupsid = g.groupsid)");
                            qry.Add("where webusersid = @webusersid");
                            qry.Add("order by usertype desc"); //2016-12-07 MY: This is a hack fix to make Usertype: user show up first. Need a better solution.
                            qry.AddParameter("@webusersid", webUsersId);

                            await qry.ExecuteAsync();
                            if (qry.RowCount > 0)
                            {
                                //identity = new ClaimsIdentity(new GenericIdentity(username, "Token"));
                                identity = new ClaimsIdentity();
                                identity.AddClaim(new Claim(AuthenticationClaimsTypes.Version, FwProgram.ServerVersion));
                                if (qry.FieldNames.Contains("webusersid"))
                                {
                                    string webusersid = qry.GetField("webusersid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(webusersid))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.WebUsersId, webusersid));
                                    }
                                }
                                if (qry.FieldNames.Contains("usersid"))
                                {
                                    string usersid = qry.GetField("usersid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(usersid))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.UsersId, usersid));
                                    }
                                }
                                if (qry.FieldNames.Contains("contactid"))
                                {
                                    string contactid = qry.GetField("contactid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(contactid))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.ContactId, contactid));
                                    }
                                }
                                if (qry.FieldNames.Contains("groupsid"))
                                {
                                    string groupsid = qry.GetField("groupsid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(groupsid))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.GroupsId, groupsid));
                                    }
                                }
                                if (qry.FieldNames.Contains("groupsdatestamp"))
                                {
                                    string groupsdatestamp = qry.GetField("groupsdatestamp").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(groupsdatestamp))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.GroupsDateStamp, groupsdatestamp));
                                    }
                                }
                                if (qry.FieldNames.Contains("usertype"))
                                {
                                    string usertype = qry.GetField("usertype").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(usertype))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.UserType, usertype));
                                    }
                                }
                                if (qry.FieldNames.Contains("personid"))
                                {
                                    string personid = qry.GetField("personid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(personid))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.PersonId, personid));
                                    }
                                }
                                if (qry.FieldNames.Contains("primarycampusid"))
                                {
                                    string campusId = qry.GetField("primarycampusid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(campusId))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.CampusId, campusId)); ;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return identity;
        }
        // --------------------------------------------------------------------------------
        public static async Task<ActionResult<OktaSessionResponseModel>> OktaVerifySession([FromBody] OktaSessionRequest request)
        {
            string url = request.Apiurl;
            FwApplicationConfig ApplicationConfig = request.appConfig;
            var client = FwJwtLogic.client;
            client.DefaultRequestHeaders.Add("Authorization", $"SSWS {ApplicationConfig.JwtIssuerOptions.OktaKey}");
            HttpResponseMessage response = await client.GetAsync(url);
            var result = response.IsSuccessStatusCode;
            client.DefaultRequestHeaders.Clear();
            return new OkObjectResult(result);
        }
        // --------------------------------------------------------------------------------
        public class ServiceTokenOptions
        {
            public List<string> ControllerIds = new List<string>();
            public List<string> MethodIds = new List<string>();
            public DateTime? Expiration = DateTime.Now.AddMinutes(15);
            public List<Claim> Claims;
        }

        public static async Task<string> GetServiceTokenAsync(FwApplicationConfig appConfig, ServiceTokenOptions serviceTokenOptions)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(AuthenticationClaimsTypes.Version, FwProgram.ServerVersion));
            claims.Add(new Claim(AuthenticationClaimsTypes.TokenType, "SERVICE"));

            if (serviceTokenOptions.ControllerIds.Count > 0)
            {
                claims.Add(new Claim(AuthenticationClaimsTypes.ControllerIdFilter, String.Join(",", serviceTokenOptions.ControllerIds)));
            }
            if (serviceTokenOptions.MethodIds.Count > 0)
            {
                claims.Add(new Claim(AuthenticationClaimsTypes.MethodIdFilter, String.Join(",", serviceTokenOptions.MethodIds)));
            }

            if (serviceTokenOptions.Claims.Count > 0)
            {
                claims.AddRange(serviceTokenOptions.Claims);
            }

            var jwt = new JwtSecurityToken(
                    issuer: appConfig.JwtIssuerOptions.Issuer,
                    audience: appConfig.JwtIssuerOptions.Audience,
                    claims: claims,
                    expires: serviceTokenOptions.Expiration,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appConfig.JwtIssuerOptions.SecretKey)), SecurityAlgorithms.HmacSha256));

            string serviceToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            await Task.CompletedTask;
            return serviceToken;
        }
        // --------------------------------------------------------------------------------
    }
}
