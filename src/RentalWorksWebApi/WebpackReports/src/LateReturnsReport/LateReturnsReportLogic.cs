using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.LateReturnsReport
{
    public class LateReturnsReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        LateReturnsReportLoader lateReturnsReportLoader = new LateReturnsReportLoader();
        public LateReturnsReportLogic()
        {
            dataLoader = lateReturnsReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderedByContactId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderedByName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryTypeId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderFromDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderFromTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderFromDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderToDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderToTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderToDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemFromDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemFromTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemFromDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemToDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemToTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ItemToDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillFromDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillToDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BillDateRange { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalItemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string BarCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SerialNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Quantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? OrderPastDue { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ItemPastDue { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? OrderDueIn { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ItemDueIn { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ItemUnitValue { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ItemUnitValueExtended { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ItemReplacementCost { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? ItemReplacementCostExtended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? OrderUnitValue { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? OrderReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
