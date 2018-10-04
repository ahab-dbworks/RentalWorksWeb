using FwStandard.Models;
using FwStandard.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FwCore.Security
{
    public class UserClaimsProvider
    {
        //---------------------------------------------------------------------------------------------
        internal static async Task<ClaimsIdentity> GetClaimsIdentity(SqlServerConfig dbConfig, string username, string password)
        {
            ClaimsIdentity identity = await FwUserClaimsProvider.GetClaimsIdentityAsync(dbConfig, username, password);
            return identity;
        }
        //---------------------------------------------------------------------------------------------
        internal static async Task<ClaimsIdentity> GetIntegrationClaimsIdentity(SqlServerConfig dbConfig, string client_id, string client_secret)
        {
            ClaimsIdentity identity = await FwUserClaimsProvider.GetIntegrationClaimsIdentity(dbConfig, client_id, client_secret);
            return identity;
        }
        //---------------------------------------------------------------------------------------------
    }
}
