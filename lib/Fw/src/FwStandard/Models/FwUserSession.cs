using FwStandard.AppManager;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
        public IEnumerable<Claim> UpdateUserClaim(string key, string value)
        {
            var identity = new ClaimsIdentity(this.User.Identity);

            identity.RemoveClaim(identity.FindFirst(key));
            identity.AddClaim(new Claim(key, value));

            return identity.Claims;
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
        // mv 2019-08-27 - This is not an appropriate claim type, which is why I have remvoed this.  I see no purpose for the server to be using a UserName or for this information to be stored in the token.
        //public string UsersName
        //{
        //    get
        //    {
        //        var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.UserName);
        //        string userName = (claim != null) ? claim.Value : string.Empty;
        //        return userName;
        //    }
        //}
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
        //------------------------------------------------------------------------------------
        public string CampusId
        {
            get
            {
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == AuthenticationClaimsTypes.CampusId);
                string campusid = (claim != null) ? claim.Value : string.Empty;
                return campusid;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
