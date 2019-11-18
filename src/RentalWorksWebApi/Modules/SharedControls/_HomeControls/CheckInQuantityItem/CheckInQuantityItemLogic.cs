using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInQuantityItem
{
    [FwLogic(Id:"3HUIkPIhnNS6")]
    public class CheckInQuantityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInQuantityItemLoader checkInQuantityItemLoader = new CheckInQuantityItemLoader();
        public CheckInQuantityItemLogic()
        {
            dataLoader = checkInQuantityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"gpsAPtjyIM6E", IsReadOnly:true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id:"KDjxvaHO02UC", IsReadOnly:true)]
        public string OrderItemId { get; set; }

        [FwLogicProperty(Id:"K9dI9DsLRzZQ", IsReadOnly:true)]
        public string ParentId { get; set; }

        [FwLogicProperty(Id:"eX9mYB1zmomX", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"eAZ0bqjMFAF7", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"YTSXol609kWr", IsReadOnly:true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id:"eAZ0bqjMFAF7", IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"PYuJ7D4SsKHF", IsReadOnly:true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id:"YTSXol609kWr", IsReadOnly:true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id:"eAZ0bqjMFAF7", IsReadOnly:true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id:"dR1NH6ImNlbx", IsReadOnly:true)]
        public decimal? QuantityOrdered { get; set; }

        [FwLogicProperty(Id:"dR1NH6ImNlbx", IsReadOnly:true)]
        public decimal? QuantityOut { get; set; }

        [FwLogicProperty(Id:"dR1NH6ImNlbx", IsReadOnly:true)]
        public decimal? Quantity { get; set; }

        [FwLogicProperty(Id:"akmrUegaEaqL", IsReadOnly:true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id:"akmrUegaEaqL", IsReadOnly:true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id:"yMfJvSc1CiMT", IsReadOnly:true)]
        public string ConsignorId { get; set; }

        [FwLogicProperty(Id:"yMfJvSc1CiMT", IsReadOnly:true)]
        public string ConsignorAgreementId { get; set; }

        [FwLogicProperty(Id:"yMfJvSc1CiMT", IsReadOnly:true)]
        public string Consignor { get; set; }

        [FwLogicProperty(Id:"yMfJvSc1CiMT", IsReadOnly:true)]
        public string VendorConsignor { get; set; }

        [FwLogicProperty(Id:"yMfJvSc1CiMT", IsReadOnly:true)]
        public string VendorConsignorColor { get; set; }

        [FwLogicProperty(Id:"hxK5UtThx3a")]
        public int? OrderPriority { get; set; }

        [FwLogicProperty(Id:"6Se8TvRbSEvU", IsReadOnly:true)]
        public string ItemOrder { get; set; }

        [FwLogicProperty(Id:"T0DeMF9OS5aM", IsReadOnly:true)]
        public bool? AllowQuantity { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
