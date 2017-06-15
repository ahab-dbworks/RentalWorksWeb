using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.AccountModels.RwUser
{
    public class RwUser
    {
        public string locationid   { get; set; }
        public string departmentid { get; set; } = string.Empty;
        public string groupsid     { get; set; } = string.Empty;
    }
}