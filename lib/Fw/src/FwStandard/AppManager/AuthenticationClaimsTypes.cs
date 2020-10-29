using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.AppManager
{
    public static class AuthenticationClaimsTypes
    {
        public const string WebUsersId = "http://www.dbworks.com/claims/webusersid";
        public const string UsersId = "http://www.dbworks.com/claims/usersid";
        public const string ContactId = "http://www.dbworks.com/claims/contactid";
        public const string GroupsId = "http://www.dbworks.com/claims/groupsid";
        public const string GroupsDateStamp = "groupsdatestamp";
        public const string UserType = "http://www.dbworks.com/claims/usertype";
        public const string PersonId = "http://www.dbworks.com/claims/personid";
        public const string DealId = "dealid";
        public const string CampusId = "campusid";
        public const string Version = "version";
        public const string TokenType = "tokentype";
        public const string ControllerIdFilter = "controlleridfilter";
        public const string MethodIdFilter = "methodidfilter";
    }

    public static class TokenTypes
    {
        public const string Service = "SERVICE";
        public const string Integration = "INTEGRATION";
    }
}
