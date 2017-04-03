using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using RentalWorksMiddleTier.Models;
using RentalWorksMiddleTier.Filters;

namespace RentalWorksMiddleTier.Controllers
{
    [AppConfigAuthorize]
    public class SqlCacheController : ApiController
    {
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("sqlcache/getdata")]
        public SqlCacheModels.GetDataResponse GetData([FromBody]SqlCacheModels.GetDataRequest request) {
            try {
                if (string.IsNullOrEmpty(request.table)) {
                    return new SqlCacheModels.GetDataResponse("{76EFB1E9-BE82-4400-9C4A-910F763E07AD}", "table is required [sqlcache/getdata]");
                }
                if ((request.uniqueids == null) || (request.uniqueids.Count == 0)) {
                    return new SqlCacheModels.GetDataResponse("{A5B92D70-490E-4DFB-BC9A-04277C8D1382}", "uniqueid is required for table: " + request.table + " [sqlcache/getdata]");
                }
                return SqlCacheManager.GetData(request);
            }
            catch(Exception ex) {
                return new SqlCacheModels.GetDataResponse("{AD612023-8094-4D90-8509-4E0EA68AB508}", ex.Message + " [sqlcache/getdata]");
            }
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("sqlcache/getcount")]
        public SqlCacheModels.GetCountResponse GetCount([FromBody]SqlCacheModels.GetCountRequest request) {
            try {
                if (string.IsNullOrEmpty(request.table)) {
                    return new SqlCacheModels.GetCountResponse("{052F66EE-9FCB-468E-9CC0-5CE207FBF38A}", "table is required [sqlcache/getcount]");
                }
                return SqlCacheManager.GetCount(request);
            }
            catch(Exception ex) {
                return new SqlCacheModels.GetCountResponse("{B8BE6CDB-49DA-4A2F-B844-B36536ABD48C}", ex.Message + " [sqlcache/getcount]");
            }
        }
        //----------------------------------------------------------------------------------------------------
        [HttpPost]
        [Route("sqlcache/refresh")]
        public void Refresh([FromBody]SqlCacheModels.RefreshRequest request)
        {
            SqlCacheManager.CacheTable(request.table);
        }

        //----------------------------------------------------------------------------------------------------
    }
}