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
                using (FwSqlCommand qry = new FwSqlCommand(conn, dbConfig.QueryTimeout))
                {
                    qry.Add("select top 1 webusersid, usersid, contactid, groupsid");
                    qry.Add("from webusersview with (nolock)");
                    qry.Add("where (upper(userloginname) = upper(@username) and upper(userpassword) = dbo.encrypt(upper(@password)))");
                    qry.Add("  or (upper(email) = upper(@username) and upper(webpassword) = dbo.encrypt(upper(@password)))");
                    qry.AddParameter("@username", username);
                    qry.AddParameter("@password", password);
                    await qry.ExecuteAsync(true);
                    if (qry.RowCount > 0)
                    {
                        string webusersid = qry.GetField("webusersid").ToString().TrimEnd();
                        string usersid = qry.GetField("usersid").ToString().TrimEnd();
                        string contactid = qry.GetField("contactid").ToString().TrimEnd();
                        string groupsid = qry.GetField("groupsid").ToString().TrimEnd();
                        identity = new ClaimsIdentity(new GenericIdentity(username, "Token"));
                        if (!string.IsNullOrEmpty(webusersid))
                        {
                            identity.AddClaim(new Claim("http://www.dbworks.com/claims/webusersid", webusersid));
                        }
                        if (!string.IsNullOrEmpty(usersid))
                        {
                            identity.AddClaim(new Claim("http://www.dbworks.com/claims/usersid", usersid));
                        }
                        if (!string.IsNullOrEmpty(contactid))
                        {
                            identity.AddClaim(new Claim("http://www.dbworks.com/claims/contactid", contactid));
                        }
                        if (!string.IsNullOrEmpty(groupsid))
                        {
                            identity.AddClaim(new Claim("http://www.dbworks.com/claims/groupsid", groupsid));
                        }
                    }
                }
            }

            return identity;
        }
    }
}
