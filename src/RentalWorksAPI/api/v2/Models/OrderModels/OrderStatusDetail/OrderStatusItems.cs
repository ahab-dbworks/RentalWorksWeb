namespace RentalWorksAPI.api.v2.Models.OrderModels.OrderStatusDetail
{
    public class OrderStatusItems
    {
        public string rectype         { get; set; }
        public string masterid        { get; set; }
        public string masterno        { get; set; }
        public string description     { get; set; }
        public int qtyordered         { get; set; }
        public int qtystaged          { get; set; }
        public int qtyout             { get; set; }
        public int qtyin              { get; set; }
        public int qtystillout        { get; set; }
        public string itemorder       { get; set; }
        public string trackedby       { get; set; }
        public string barcode         { get; set; }
        public string serialno        { get; set; }
        public string vendorconsignor { get; set; }
        public string outdatetime     { get; set; }
        public string indatetime      { get; set; }
    }
}