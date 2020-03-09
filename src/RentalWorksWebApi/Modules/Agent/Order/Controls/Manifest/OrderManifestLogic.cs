using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.OrderManifest
{
    [FwLogic(Id: "9P3FcBfJdRX5M")]
    public class OrderManifestLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderManifestLoader orderManifestLoader = new OrderManifestLoader();
        public OrderManifestLogic()
        {
            dataLoader = orderManifestLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "aAsPI7LlpVnkC", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "AcyU1lCQLc43R", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "ajdl9fI0kT6On", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "aNc86sboGE4cl", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "aOVqb6qOL4RPf", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "Atr8IiTaES1cQ", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "awiygoQfK23S3", IsReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwLogicProperty(Id: "aZrgtn74rrA66", IsReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        [FwLogicProperty(Id: "B2R99dahqtc3z", IsReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwLogicProperty(Id: "b7CscvdDbtl5I", IsReadOnly: true)]
        public string CountryOfOrigin { get; set; }
        [FwLogicProperty(Id: "baBqT5aCbkck8", IsReadOnly: true)]
        public decimal? ValuePerItem { get; set; }
        [FwLogicProperty(Id: "bc3T5x2532zHw", IsReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwLogicProperty(Id: "bE3ezuJOGNaj8", IsReadOnly: true)]
        public string DimensionsLWH { get; set; }
        [FwLogicProperty(Id: "BwzcG1FurKxQA", IsReadOnly: true)]
        public string Barcode { get; set; }
        [FwLogicProperty(Id: "C7PJb2MhwIvK1", IsReadOnly: true)]
        public string MfgSerial { get; set; }
        [FwLogicProperty(Id: "cfpUE2sF0VN3t", IsReadOnly: true)]
        public string MfgPartNo { get; set; }
        [FwLogicProperty(Id: "CHOHxp0guUrXc", IsReadOnly: true)]
        public decimal? ValueExtended { get; set; }
        [FwLogicProperty(Id: "cj839U8ReKOiX", IsReadOnly: true)]
        public int? WeightLbs { get; set; }
        [FwLogicProperty(Id: "CpWsvL6x4dIoL", IsReadOnly: true)]
        public int? WeightOz { get; set; }
        [FwLogicProperty(Id: "cpXZ3wltAhndU", IsReadOnly: true)]
        public int? WeightKg { get; set; }
        [FwLogicProperty(Id: "CrfHlBnifJiSz", IsReadOnly: true)]
        public int? WeightGr { get; set; }
        [FwLogicProperty(Id: "CvzBFv0ixWVV3", IsReadOnly: true)]
        public int? ExtendedWeightLbs { get; set; }
        [FwLogicProperty(Id: "D2eVF6735pf03", IsReadOnly: true)]
        public int? ExtendedWeightOz { get; set; }
        [FwLogicProperty(Id: "d64pATk13Vu50", IsReadOnly: true)]
        public int? ExtendedWeightKg { get; set; }
        [FwLogicProperty(Id: "dRh41HpRFx94d", IsReadOnly: true)]
        public int? ExtendedWeightGr { get; set; }
        [FwLogicProperty(Id: "oR6cF6p1PfvE", IsReadOnly: true)]
        public bool? ManifestShippingContainer { get; set; }
        [FwLogicProperty(Id: "GUsCUE3Ueazh", IsReadOnly: true)]
        public bool? ManifestStandAloneItem { get; set; }
        [FwLogicProperty(Id: "SID4zA7y9SGb", IsReadOnly: true)]
        public decimal? OrderValueTotal { get; set; }
        [FwLogicProperty(Id: "avbjD5D2KDza", IsReadOnly: true)]
        public decimal? OrderReplacementTotal { get; set; }
        [FwLogicProperty(Id: "G1NVAtRw5Rgc", IsReadOnly: true)]
        public decimal? OwnedValueTotal { get; set; }
        [FwLogicProperty(Id: "cB8NWA0rJsqE", IsReadOnly: true)]
        public decimal? OwnedReplacementTotal { get; set; }
        [FwLogicProperty(Id: "oM2vGwWqjDxH", IsReadOnly: true)]
        public decimal? SubValueTotal { get; set; }
        [FwLogicProperty(Id: "Wk8Qbkftp9fc", IsReadOnly: true)]
        public decimal? SubReplacementTotal { get; set; }
        [FwLogicProperty(Id: "70flMhApLjIW", IsReadOnly: true)]
        public decimal? ShippingContainerTotal { get; set; }
        [FwLogicProperty(Id: "AxYJ403JdA0L", IsReadOnly: true)]
        public decimal? ShippingItemTotal { get; set; }
        [FwLogicProperty(Id: "Cjqzn7qpG0yQ", IsReadOnly: true)]
        public decimal? PieceCountTotal { get; set; }
        [FwLogicProperty(Id: "qMMbcj6NMkqN", IsReadOnly: true)]
        public decimal? StandAloneItemTotal { get; set; }
        [FwLogicProperty(Id: "5OCDudsxkMJy", IsReadOnly: true)]
        public decimal? TotalExtendedWeightLbs { get; set; }
        [FwLogicProperty(Id: "OWIjOCCeknqC", IsReadOnly: true)]
        public decimal? TotalExtendedWeightOz { get; set; }
        [FwLogicProperty(Id: "8VsYFMuWuneo", IsReadOnly: true)]
        public decimal? TotalExtendedWeightKg { get; set; }
        [FwLogicProperty(Id: "WuBss6unVNfn", IsReadOnly: true)]
        public decimal? TotalExtendedWeightGr { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
