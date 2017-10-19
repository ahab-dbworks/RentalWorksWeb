using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace RentalWorksWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class RwController : Controller  //todo: create FwController to inherit from
    {
        protected readonly ApplicationConfig _appConfig;
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
        public RwController(IOptions<ApplicationConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
    }
}