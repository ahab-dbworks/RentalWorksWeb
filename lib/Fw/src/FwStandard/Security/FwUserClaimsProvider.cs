using FwStandard.Models;
using FwStandard.SqlServer;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FwStandard.Security
{
    public class FwUserClaimsProvider
    {
        public static async Task<ClaimsIdentity> GetClaimsIdentityAsync(SqlServerConfig dbConfig, string username, string password)
        {
            ClaimsIdentity identity = null;
            using (FwSqlConnection conn = new FwSqlConnection(dbConfig.ConnectionString))
            {
                using (FwSqlCommand qryLogin = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    //qry.Add("select top 1 *");
                    //qry.Add("from webusersview with (nolock)");
                    //qry.Add("where (upper(userloginname) = upper(@username) and upper(userpassword) = dbo.encrypt(upper(@password)))");
                    //qry.Add("  or (upper(email) = upper(@username) and upper(webpassword) = dbo.encrypt(upper(@password)))");
                    //qry.AddParameter("@username", username);
                    //qry.AddParameter("@password", password);

                    //jh 04/13/2018 splitting the above query into two for speed.  Takes to long to decrypt passwords when there are 35,000+ entries in the webusersview

                    string webUsersId = "";
                    string userLoginName = "";
                    string email = "";
                    bool loginWithUserName = false;
                    bool loginWithEmail = false;

                    qryLogin.Add("select top 1 webusersid, userloginname, email");
                    qryLogin.Add("from webusersview with (nolock)");
                    qryLogin.Add("where (upper(userloginname) = upper(@username) ");
                    qryLogin.Add("  or (upper(email) = upper(@username))) ");
                    qryLogin.AddParameter("@username", username);
                    await qryLogin.ExecuteAsync(true);
                    if (qryLogin.RowCount > 0)
                    {
                        webUsersId = qryLogin.GetField("webusersid").ToString().TrimEnd();
                        userLoginName = qryLogin.GetField("userloginname").ToString().TrimEnd();
                        email = qryLogin.GetField("email").ToString().TrimEnd();

                        loginWithUserName = (username.Equals(userLoginName));
                        loginWithEmail = (username.Equals(email));


                        using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                        {
                            qry.Add("select top 1 *");
                            qry.Add("from webusersview with (nolock)");
                            qry.Add("where webusersid = @webusersid");
                            if (loginWithEmail)
                            {
                                qry.Add(" and webpassword = dbo.encrypt(@password)");
                            }
                            else
                            {
                                qry.Add(" and userpassword = dbo.encrypt(@password)");
                            }
                            qry.AddParameter("@webusersid", webUsersId);
                            qry.AddParameter("@password", password);

                            await qry.ExecuteAsync(true);
                            if (qry.RowCount > 0)
                            {
                                identity = new ClaimsIdentity(new GenericIdentity(username, "Token"));
                                if (qry.FieldNames.Contains("webusersid"))
                                {
                                    string webusersid = qry.GetField("webusersid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(webusersid))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/webusersid", webusersid));
                                    }
                                }
                                if (qry.FieldNames.Contains("usersid"))
                                {
                                    string usersid = qry.GetField("usersid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(usersid))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/usersid", usersid));
                                    }
                                }
                                if (qry.FieldNames.Contains("contactid"))
                                {
                                    string contactid = qry.GetField("contactid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(contactid))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/contactid", contactid));
                                    }
                                }
                                if (qry.FieldNames.Contains("groupsid"))
                                {
                                    string groupsid = qry.GetField("groupsid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(groupsid))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/groupsid", groupsid));
                                    }
                                }
                                if (qry.FieldNames.Contains("usertype"))
                                {
                                    string usertype = qry.GetField("usertype").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(usertype))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/usertype", usertype));
                                    }
                                }
                                if (qry.FieldNames.Contains("personid"))
                                {
                                    string personid = qry.GetField("personid").ToString().TrimEnd();
                                    if (!string.IsNullOrEmpty(personid))
                                    {
                                        identity.AddClaim(new Claim("http://www.dbworks.com/claims/personid", personid));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return identity;
        }
    }
}
