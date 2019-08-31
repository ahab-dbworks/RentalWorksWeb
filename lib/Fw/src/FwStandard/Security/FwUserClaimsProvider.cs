using FwStandard.Models;
using FwStandard.SqlServer;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FwStandard.Security
{
    public class FwUserClaimsProvider
    {
        //---------------------------------------------------------------------------------------------
        public static async Task<ClaimsIdentity> GetClaimsIdentityAsync(SqlServerConfig dbConfig, string username, string password)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qryAuthenticate = new FwSqlCommand(conn, "webauthenticate", dbConfig.QueryTimeout))
                {
                    //qry.Add("select top 1 *");
                    //qry.Add("from webusersview with (nolock)");
                    //qry.Add("where (upper(userloginname) = upper(@username) and upper(userpassword) = dbo.encrypt(upper(@password)))");
                    //qry.Add("  or (upper(email) = upper(@username) and upper(webpassword) = dbo.encrypt(upper(@password)))");
                    //qry.AddParameter("@username", username);
                    //qry.AddParameter("@password", password);

                    //jh 04/13/2018 splitting the above query into two for speed.  Takes to long to decrypt passwords when there are 35,000+ entries in the webusersview

                    FwSqlCommand qryEncrypt = new FwSqlCommand(conn, dbConfig.QueryTimeout);
                    qryEncrypt.Add("select value = dbo.encrypt(@data)");
                    qryEncrypt.AddParameter("@data", password.ToUpper());
                    await qryEncrypt.ExecuteAsync();
                    string webpassword = qryEncrypt.GetField("value").ToString().TrimEnd();

                    string webUsersId = string.Empty, errmsg = string.Empty;
                    int    errno = 0;

                    qryAuthenticate.AddParameter("@userlogin",         username);
                    qryAuthenticate.AddParameter("@userloginpassword", webpassword);
                    qryAuthenticate.AddParameter("@webusersid",        System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errno",             System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errmsg",            System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    await qryAuthenticate.ExecuteAsync();
                    webUsersId = qryAuthenticate.GetParameter("@webusersid").ToString().TrimEnd();
                    errno      = qryAuthenticate.GetParameter("@errno").ToInt32();
                    errmsg     = qryAuthenticate.GetParameter("@errmsg").ToString().TrimEnd();

                    if (!string.IsNullOrEmpty(webUsersId) && (errno.Equals(0)))
                    {
                        using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                        {
                            qry.Add("select top 1 *");
                            qry.Add("from webusersview with (nolock)");
                            qry.Add("where webusersid = @webusersid");
                            qry.Add("order by usertype desc"); //2016-12-07 MY: This is a hack fix to make Usertype: user show up first. Need a better solution.
                            qry.AddParameter("@webusersid", webUsersId);

                            await qry.ExecuteAsync();
                            if (qry.RowCount > 0)
                            {
                                //identity = new ClaimsIdentity(new GenericIdentity(username, "Token"));
                                identity = new ClaimsIdentity();
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
                                if (qry.FieldNames.Contains("fullname"))
                                {
                                    string userName = qry.GetField("fullname").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(userName))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.UserName, userName));
                                    }
                                }
                                if (qry.FieldNames.Contains("primarycampusid"))
                                {
                                    string campusId = qry.GetField("primarycampusid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(campusId))
                                    {
                                        identity.AddClaim(new Claim(AuthenticationClaimsTypes.CampusId, campusId));;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return identity;
        }
        //---------------------------------------------------------------------------------------------
        public static async Task<ClaimsIdentity> GetIntegrationClaimsIdentity(SqlServerConfig dbConfig, string client_id, string client_secret)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qryAuthenticate = new FwSqlCommand(conn, "appintegrationauthenticate", dbConfig.QueryTimeout))
                {
                    qryAuthenticate.AddParameter("@clientid",     client_id);
                    qryAuthenticate.AddParameter("@clientsecret", client_secret);
                    qryAuthenticate.AddParameter("@dealid",       System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@campusid",     System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errno",        System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errmsg",       System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
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
        public static class AuthenticationClaimsTypes
        {
            public const string WebUsersId = "http://www.dbworks.com/claims/webusersid";
            public const string UsersId    = "http://www.dbworks.com/claims/usersid";
            public const string ContactId  = "http://www.dbworks.com/claims/contactid";
            public const string GroupsId   = "http://www.dbworks.com/claims/groupsid";
            public const string UserType   = "http://www.dbworks.com/claims/usertype";
            public const string PersonId   = "http://www.dbworks.com/claims/personid";
            public const string DealId     = "dealid";
            public const string CampusId   = "campusid";
            public const string UserName   = "username";
        }
        //---------------------------------------------------------------------------------------------
    }
}
