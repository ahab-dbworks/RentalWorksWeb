using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatusHistory
    {
        [DataType(DataType.Text)]
        public string type     { get; set; }

        [DataType(DataType.DateTime)]
        public string datetime { get; set; }

        [DataType(DataType.Text)]
        public string orderid  { get; set; }

        [DataType(DataType.Text)]
        public string orderno  { get; set; }

        [DataType(DataType.Text)]
        public string dealname { get; set; }

        public int qty         { get; set; }
    }
}