using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.OrderModels.OrdersAndItems
{
    public class OrdersAndItems
    {
        public string locationid             { get; set; }
        public string departmentid           { get; set; } = string.Empty;
        public string lastmodifiedfromdate   { get; set; } = string.Empty;
        public string lastmodifiedtodate     { get; set; } = string.Empty;
        public string includeavailabilityqty { get; set; } = string.Empty;
        public string orderid                { get; set; } = string.Empty;
        public List<string> agentid          { get; set; } = new List<string>();
        public List<string> status           { get; set; } = new List<string>();
        public List<string> dealid           { get; set; } = new List<string>();
    }
}