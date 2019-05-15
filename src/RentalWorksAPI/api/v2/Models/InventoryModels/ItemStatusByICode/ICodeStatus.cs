using System.Collections.Generic;

namespace RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusByICode
{
    //----------------------------------------------------------------------------------------------------
    public class ItemStatusByICode
    {
        public string warehouseid           { get; set; }
        public string masterid              { get; set; }
    }
    //----------------------------------------------------------------------------------------------------
    public class ItemStatusByICodeResponse
    {
        public string masterid              { get; set; }
        public string masterno              { get; set; }
        public string master                { get; set; }
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