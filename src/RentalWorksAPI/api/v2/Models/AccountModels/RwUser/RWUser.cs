using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.AccountModels.RwUser
{
    public class RwUser
    {
        [DataType(DataType.Text)]
        public string locationid   { get; set; }

        [DataType(DataType.Text)]
        public string departmentid { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string groupsid     { get; set; } = string.Empty;
    }
}