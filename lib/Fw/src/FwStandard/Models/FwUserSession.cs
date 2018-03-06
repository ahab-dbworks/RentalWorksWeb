using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class FwUserSession
    {
        public string WebUsersId { get; set; } = "";
        public string GroupsId { get; set; } = "";
        public string UsersId { get; set; } = "";
        public string ContactId { get; set; } = "";
        public string PersonId { get; set; } = "";
        public string UserType { get; set; } = "";
    }
}
