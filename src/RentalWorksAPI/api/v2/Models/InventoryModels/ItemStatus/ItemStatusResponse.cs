using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatusResponse
    {
        public string rentalitemid                  { get; set; }
        public string masterid                      { get; set; }
        public string masterno                      { get; set; }
        public string master                        { get; set; }
        public string mfgserial                     { get; set; }
        public string rfid                          { get; set; }
        public string barcode                       { get; set; }
        public string status                        { get; set; }
        public List<ItemStatusHistory> transactions { get; set; }
    }
}