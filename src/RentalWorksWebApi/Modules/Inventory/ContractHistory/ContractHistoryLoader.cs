using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.ContractHistory
{
    [FwSqlTable("inventorycontracthistoryview")]
    public class ContractHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertranid", modeltype: FwDataTypes.Integer)]
        public int? OrderTranId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean)]
        public bool? InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfrom", modeltype: FwDataTypes.Text)]
        public string AvailableFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metered", modeltype: FwDataTypes.Boolean)]
        public bool? IsMetered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedbyweight", modeltype: FwDataTypes.Boolean)]
        public bool? IsTrackedByWeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedbylength", modeltype: FwDataTypes.Text)]
        public string IsTrackedByLength { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outcontractid", modeltype: FwDataTypes.Text)]
        public string OutContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outcontractno", modeltype: FwDataTypes.Text)]
        public string OutContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsuspend", modeltype: FwDataTypes.Boolean)]
        public bool? IsOutSuspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdatetime", modeltype: FwDataTypes.Date)]
        public string OutDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outusername", modeltype: FwDataTypes.Text)]
        public string OutUserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouseid", modeltype: FwDataTypes.Text)]
        public string OutWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwhcode", modeltype: FwDataTypes.Text)]
        public string OutWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouse", modeltype: FwDataTypes.Text)]
        public string OutWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractno", modeltype: FwDataTypes.Text)]
        public string InContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "insuspend", modeltype: FwDataTypes.Boolean)]
        public bool? IsInSuspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "indatetime", modeltype: FwDataTypes.Date)]
        public string InDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inusersid", modeltype: FwDataTypes.Text)]
        public string InUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inusername", modeltype: FwDataTypes.Text)]
        public string InUserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwarehouseid", modeltype: FwDataTypes.Text)]
        public string InWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwhcode", modeltype: FwDataTypes.Text)]
        public string InWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwarehouse", modeltype: FwDataTypes.Text)]
        public string InWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemstatus", modeltype: FwDataTypes.Boolean)]
        public bool? ItemStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "torepair", modeltype: FwDataTypes.Boolean)]
        public bool? IsToRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerrentalitemid", modeltype: FwDataTypes.Text)]
        public string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assethoursout", modeltype: FwDataTypes.Integer)]
        public int? AssetHoursOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assethoursin", modeltype: FwDataTypes.Integer)]
        public int? AssetHoursIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikesout", modeltype: FwDataTypes.Integer)]
        public int? StrikesOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "strikesin", modeltype: FwDataTypes.Integer)]
        public int? StrikesIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "candlesout", modeltype: FwDataTypes.Integer)]
        public int? CandlesOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "candlesin", modeltype: FwDataTypes.Integer)]
        public int? CandlesIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterout", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterin", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours1out", modeltype: FwDataTypes.Integer)]
        public int? LampHours1Out { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours1in", modeltype: FwDataTypes.Integer)]
        public int? LampHours1In { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours2out", modeltype: FwDataTypes.Integer)]
        public int? LampHours2Out { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours2in", modeltype: FwDataTypes.Integer)]
        public int? LampHours2In { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours3out", modeltype: FwDataTypes.Integer)]
        public int? LampHours3Out { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours3in", modeltype: FwDataTypes.Integer)]
        public int? LampHours3In { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours4out", modeltype: FwDataTypes.Integer)]
        public int? LampHours4Out { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lamphours4in", modeltype: FwDataTypes.Integer)]
        public int? LampHours4In { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("InventoryId", "masterid", select, request); 
            addFilterToSelect("ItemId", "rentalitemid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
