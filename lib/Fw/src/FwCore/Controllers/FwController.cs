﻿using FwStandard.Models;
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
        protected FwUserSession UserSession
        {
            get
            {
                var userSession = new FwUserSession(){UsersId = this.UsersId, GroupsId = this.GroupsId, WebUsersId = this.WebUsersId };
                return userSession;
            }
        }
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
        }
        //------------------------------------------------------------------------------------
    }
}