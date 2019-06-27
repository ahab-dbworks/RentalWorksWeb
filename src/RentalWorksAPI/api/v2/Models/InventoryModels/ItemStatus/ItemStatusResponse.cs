using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus
{
    public class ItemStatusResponse
    {
        [DataType(DataType.Text)]
        public string rentalitemid                  { get; set; }

        [DataType(DataType.Text)]
        public string masterid                      { get; set; }

        [DataType(DataType.Text)]
        public string masterno                      { get; set; }

        [DataType(DataType.Text)]
        public string master                        { get; set; }

        [DataType(DataType.Text)]
        public string mfgserial                     { get; set; }

        [DataType(DataType.Text)]
        public string rfid                          { get; set; }

        [DataType(DataType.Text)]
        public string barcode                       { get; set; }

        [DataType(DataType.Text)]
        public string status                        { get; set; }

        public List<ItemStatusHistory> transactions { get; set; }
    }
}