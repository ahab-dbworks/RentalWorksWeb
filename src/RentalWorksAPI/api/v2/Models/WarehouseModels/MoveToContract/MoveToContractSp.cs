using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract
{
    public class MoveToContractSp
    {
        public string masteritemid { get; set; }
        public int statuscode { get; set; }
        public string statusmessage { get; set; }
        public string barcode { get; set; }
        public string masterid { get; set; }
        public int quantity { get; set; }
        public DateTime datestamp { get; set;}
    }
}