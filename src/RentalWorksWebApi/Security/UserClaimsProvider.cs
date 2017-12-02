using FwStandard.Models;
using FwStandard.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Security
{
    public class UserClaimsProvider
    {
        internal static async Task<ClaimsIdentity> GetClaimsIdentity(SqlServerConfig dbConfig, string username, string password)
        {
            ClaimsIdentity identity = await FwUserClaimsProvider.GetClaimsIdentityAsync(dbConfig, username, password);
            return identity;
        }
    }
}
