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
        protected readonly FwApplicationConfig AppConfig;
        //------------------------------------------------------------------------------------
        protected FwUserSession UserSession
        {
            get
            {
                var userSession = new FwUserSession() {
                    UsersId = this.UsersId,
                    GroupsId = this.GroupsId,
                    WebUsersId = this.WebUsersId,
                    UserType = this.UserType,
                    ContactId = this.ContactId,
                    PersonId = this.PersonId
                };
                return userSession;
            }
        }
        //------------------------------------------------------------------------------------
        private string UsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/usersid");
                string usersid = (claim != null) ? claim.Value : string.Empty;
                return usersid;
            }
        }
        //------------------------------------------------------------------------------------
        private string WebUsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/webusersid");
                string webusersid = (claim != null) ? claim.Value : string.Empty;
                return webusersid;
            }
        }
        //------------------------------------------------------------------------------------
        private string GroupsId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/groupsid");
                string groupsid = (claim != null) ? claim.Value : string.Empty;
                return groupsid;
            }
        }
        //------------------------------------------------------------------------------------
        private string UserType
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/usertype");
                string usertype = (claim != null) ? claim.Value : string.Empty;
                return usertype;
            }
        }
        //------------------------------------------------------------------------------------
        private string ContactId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/contactid");
                string contactid = (claim != null) ? claim.Value : string.Empty;
                return contactid;
            }
        }
        //------------------------------------------------------------------------------------
        private string PersonId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == "http://www.dbworks.com/claims/personid");
                string personid = (claim != null) ? claim.Value : string.Empty;
                return personid;
            }
        }
        //------------------------------------------------------------------------------------
        public FwController(IOptions<FwApplicationConfig> appConfig)
        {
            AppConfig = appConfig.Value;
        }
        //------------------------------------------------------------------------------------
    }
}