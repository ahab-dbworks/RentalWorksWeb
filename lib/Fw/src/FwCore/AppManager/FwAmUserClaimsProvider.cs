using FwCore.Api;
using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FwCore.AppManager
{
    public class FwAmUserClaimsProvider
    {
        //---------------------------------------------------------------------------------------------
        internal static async Task<ClaimsIdentity> GetClaimsIdentity(SqlServerConfig dbConfig, string WebUsersId)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    qry.Add("select top 1 wuv.*, groupsdatestamp = g.datestamp");
                    qry.Add("from webusersview wuv with (nolock)");
                    qry.Add("  join groups g  with (nolock) on (wuv.groupsid = g.groupsid)");
                    qry.Add("where webusersid = @webusersid");
                    qry.Add("order by usertype desc"); //2016-12-07 MY: This is a hack fix to make Usertype: user show up first. Need a better solution.
                    qry.AddParameter("@webusersid", WebUsersId);

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
            return identity;
        }
        //---------------------------------------------------------------------------------------------
        internal static async Task<ClaimsIdentity> GetIntegrationClaimsIdentity(SqlServerConfig dbConfig, string client_id, string client_secret)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qryAuthenticate = new FwSqlCommand(conn, "appintegrationauthenticate", dbConfig.QueryTimeout))
                {
                    qryAuthenticate.AddParameter("@clientid", client_id);
                    qryAuthenticate.AddParameter("@clientsecret", client_secret);
                    qryAuthenticate.AddParameter("@dealid", System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@campusid", System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errno", System.Data.SqlDbType.Int, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errmsg", System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    await qryAuthenticate.ExecuteAsync();

                    if (qryAuthenticate.GetParameter("@errno").ToInt32().Equals(0))
                    {
                        identity = new ClaimsIdentity();

                        string dealid = qryAuthenticate.GetParameter("@dealid").ToString().TrimEnd();
                        if (!string.IsNullOrEmpty(dealid))
                        {
                            identity.AddClaim(new Claim(AuthenticationClaimsTypes.DealId, dealid));
                        }

                        string campusid = qryAuthenticate.GetParameter("@campusid").ToString().TrimEnd();
                        if (!string.IsNullOrEmpty(campusid))
                        {
                            identity.AddClaim(new Claim(AuthenticationClaimsTypes.CampusId, campusid));
                        }
                    }
                }
            }

            return identity;
        }
        //---------------------------------------------------------------------------------------------
    }
}
