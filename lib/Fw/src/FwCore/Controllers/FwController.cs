using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace FwCore.Controllers
{
    [Route("api/v1/[controller]")]
    public class FwController : Controller  //todo: create FwController to inherit from
    {
        protected readonly FwApplicationConfig _appConfig;
        //------------------------------------------------------------------------------------
        protected string UsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/usersid");
                string usersid = (claim != null) ? claim.Value : string.Empty;
                return usersid;
            }
        }
        //------------------------------------------------------------------------------------
        protected string WebUsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/webusersid");
                string webusersid = (claim != null) ? claim.Value : string.Empty;
                return webusersid;
            }
        }
        //------------------------------------------------------------------------------------
        protected string GroupsId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/groupsid");
                string groupsid = (claim != null) ? claim.Value : string.Empty;
                return groupsid;
            }
        }
        //------------------------------------------------------------------------------------
        public FwController(IOptions<FwApplicationConfig> appConfig)
        {
            _appConfig = appConfig.Value;

            if (FwSqlSelect.PagingCompatibility == FwSqlSelect.PagingCompatibilities.AutoDetect)
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.Value.DatabaseSettings.ConnectionString))
                {
                    bool isGte = FwSqlData.IsSqlVersionGreaterThanOrEqualTo(conn, appConfig.Value.DatabaseSettings, 2012).Result;
                    if (isGte)
                    {
                        FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.Sql2012;
                    }
                    else
                    {
                        FwSqlSelect.PagingCompatibility = FwSqlSelect.PagingCompatibilities.PreSql2012;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}