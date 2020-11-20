using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Agent.PurchaseOrderItemVendorInvoiceStatus
{
    [FwSqlTable("poitemtotalwebview")]
    public class PurchaseOrderItemVendorInvoiceStatusLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poitemid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string SubOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string SubOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "netqtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? NetQuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreceived", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReceived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityRemaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyapproved", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityApproved { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreturned", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReturned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreturnable", modeltype: FwDataTypes.Integer)]
        public int? QuantityReturnable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtycanapprove", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityCanApprove { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "porate", modeltype: FwDataTypes.Decimal)]
        public decimal? Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podiscountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "podaysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalpocost", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreceivedcost", modeltype: FwDataTypes.Decimal)]
        public decimal? ReceivedCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalinvcost", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalInvoiced { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pocostremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? CostRemaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargetype", modeltype: FwDataTypes.Text)]
        public string ChargeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date)]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date)]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.Decimal)]
        public decimal? Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxreceived", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxReceived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxRemaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalinvtax", modeltype: FwDataTypes.Decimal)]
        public decimal? InvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalinvcostwtax", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalInvoiceWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalpocostwtax", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalCostWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreceivedcostwtax", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalReceivedWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pocostremainingwtax", modeltype: FwDataTypes.Decimal)]
        public decimal? TotalRemainingWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("PurchaseOrderId", "poid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
