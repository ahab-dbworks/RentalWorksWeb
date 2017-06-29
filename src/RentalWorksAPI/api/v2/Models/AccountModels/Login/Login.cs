using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.AccountModels.Login
{
    public class Login
    {
        public string domain   { get; set; } = string.Empty;
        public string username { get; set; }
        public string password { get; set; }
    }
}