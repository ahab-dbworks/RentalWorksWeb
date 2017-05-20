using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class ICode
    {
        public string masterid                { get; set; }
        public string warehouseid             { get; set; }
        public int total                      { get; set; }
        public int consigned                  { get; set; }
        public int inqty                      { get; set; }
        public int incontainer                { get; set; }
        public int qcrequired                 { get; set; }
        public int staged                     { get; set; }
        public int outqty                     { get; set; }
        public int inrepair                   { get; set; }
        public List<Transaction> transactions { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Transaction
    {
        public string type     { get; set; }
        public string datetime { get; set; }
        public string orderid  { get; set; }
        public string orderno  { get; set; }
        public string dealname { get; set; }
        public int qty         { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}