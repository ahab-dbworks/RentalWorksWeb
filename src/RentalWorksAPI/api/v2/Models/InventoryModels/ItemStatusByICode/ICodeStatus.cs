using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusByICode
{
    //----------------------------------------------------------------------------------------------------
    public class ItemStatusByICode
    {
        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        [DataType(DataType.Text)]
        public string masterid              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ItemStatusByICodeResponse
    {
        [DataType(DataType.Text)]
        public string masterid              { get; set; }

        [DataType(DataType.Text)]
        public string masterno              { get; set; }

        [DataType(DataType.Text)]
        public string master                { get; set; }

        [DataType(DataType.Text)]
        public string warehouseid           { get; set; }

        public List<Barcode> barcodes       { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class Barcode
    {
        public string barcode             { get; set; }
        public string status              { get; set; }
        public string orderid             { get; set; }
        public string orderno             { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
}