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
                    await qryEncrypt.ExecuteAsync(true);
                    string webpassword = qryEncrypt.GetField("value").ToString().TrimEnd();

                    string webUsersId = string.Empty, errmsg = string.Empty;
                    int    errno = 0;

                    qryAuthenticate.AddParameter("@userlogin",         username);
                    qryAuthenticate.AddParameter("@userloginpassword", webpassword);
                    qryAuthenticate.AddParameter("@webusersid",        System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errno",             System.Data.SqlDbType.Int,      System.Data.ParameterDirection.Output);
                    qryAuthenticate.AddParameter("@errmsg",            System.Data.SqlDbType.NVarChar, System.Data.ParameterDirection.Output);
                    await qryAuthenticate.ExecuteAsync(true);
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
