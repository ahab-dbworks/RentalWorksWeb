using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.CheckInQuantityItem
{
    public class CheckInQuantityItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CheckInQuantityItemLoader checkInQuantityItemLoader = new CheckInQuantityItemLoader();
        public CheckInQuantityItemLogic()
        {
            dataLoader = checkInQuantityItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ParentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemClass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICodeColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DescriptionColor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOrdered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? QuantityOut { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Quantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Consignor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorConsignor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string VendorConsignorColor { get; set; }
        public int? OrderPriority { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? AllowQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
