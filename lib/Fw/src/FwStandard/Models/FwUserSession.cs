﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static FwStandard.Security.FwUserClaimsProvider;

namespace FwStandard.Models
{
    public class FwUserSession
    {
        public Dictionary<string, object> RequestFields { get; private set; } = new Dictionary<string, object>();

        ClaimsPrincipal User { get; set; }
        public FwUserSession(ClaimsPrincipal user)
        {
            this.User = user;
        }
        //------------------------------------------------------------------------------------
        public string UsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.UsersId);
                string usersid = (claim != null) ? claim.Value : string.Empty;
                return usersid;
            }
        }
        //------------------------------------------------------------------------------------
        public string WebUsersId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.WebUsersId);
                string webusersid = (claim != null) ? claim.Value : string.Empty;
                return webusersid;
            }
        }
        //------------------------------------------------------------------------------------
        public string GroupsId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.GroupsId);
                string groupsid = (claim != null) ? claim.Value : string.Empty;
                return groupsid;
            }
        }
        //------------------------------------------------------------------------------------
        public string UserType
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.UserType);
                string usertype = (claim != null) ? claim.Value : string.Empty;
                return usertype;
            }
        }
        //------------------------------------------------------------------------------------
        public string ContactId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.ContactId);
                string contactid = (claim != null) ? claim.Value : string.Empty;
                return contactid;
            }
        }
        //------------------------------------------------------------------------------------
        public string PersonId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.PersonId);
                string personid = (claim != null) ? claim.Value : string.Empty;
                return personid;
            }
        }
    }
}
