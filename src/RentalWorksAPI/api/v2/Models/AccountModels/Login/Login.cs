using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.AccountModels.Login
{
    public class Login
    {
        [DataType(DataType.Text)]
        public string domain   { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string username { get; set; }

        [DataType(DataType.Text)]
        public string password { get; set; }
    }
}