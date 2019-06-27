using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models
{
    //----------------------------------------------------------------------------------------------------
    public class ICode
    {
        [DataType(DataType.Text)]
        public string masterid                { get; set; }

        [DataType(DataType.Text)]
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
        [DataType(DataType.Text)]
        public string type             { get; set; }

        [DataType(DataType.Text)]
        public string activitydatetime { get; set; }

        [DataType(DataType.Text)]
        public string orderid          { get; set; }

        [DataType(DataType.Text)]
        public string orderno          { get; set; }

        [DataType(DataType.Text)]
        public string deal             { get; set; }

        public int qty                 { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}