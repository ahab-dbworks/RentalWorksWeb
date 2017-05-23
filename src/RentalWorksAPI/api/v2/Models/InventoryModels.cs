using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class ICode
    {
        public string masterid                { get; set; }
        public string warehouseid             { get; set; }
        public int qty                        { get; set; }
        public int qtyconsigned               { get; set; }
        public int qtyin                      { get; set; }
        public int qtyincontainer             { get; set; }
        public int qtyqcrequired              { get; set; }
        public int qtystaged                  { get; set; }
        public int qtyout                     { get; set; }
        public int qtyinrepair                { get; set; }
        public List<Transaction> transactions { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Transaction
    {
        public string type     { get; set; }
        public string datetime { get; set; }
        public string orderid  { get; set; }
        public string orderno  { get; set; }
        public string deal     { get; set; }
        public int qty         { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}