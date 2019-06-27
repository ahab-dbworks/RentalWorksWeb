using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrdersAndItems
{
    public class OrdersAndItems
    {
        [DataType(DataType.Text)]
        public string locationid             { get; set; }

        [DataType(DataType.Text)]
        public string departmentid           { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string lastmodifiedfromdate   { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string lastmodifiedtodate     { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string orderid                { get; set; } = string.Empty;

        public List<string> agentid          { get; set; } = new List<string>();

        public List<string> status           { get; set; } = new List<string>();

        public List<string> dealid           { get; set; } = new List<string>();
    }
}