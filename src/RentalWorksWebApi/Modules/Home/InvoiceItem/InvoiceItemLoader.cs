using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using WebApi.Logic;

namespace WebApi.Modules.Home.InvoiceItem
{
    [FwSqlTable("dbo.funcinvoiceitemgrid(@invoiceid,@rectype)")]
    public class InvoiceItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InvoiceItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
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
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromtime", modeltype: FwDataTypes.Text)]
        public string FromTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totime", modeltype: FwDataTypes.Text)]
        public string ToTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "days", modeltype: FwDataTypes.Decimal)]
        public decimal? Days { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cost", modeltype: FwDataTypes.Decimal)]
        public decimal? Cost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "daysinwk", modeltype: FwDataTypes.Decimal)]
        public decimal? DaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountamt", modeltype: FwDataTypes.Decimal)]
        public decimal? DiscountAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "split", modeltype: FwDataTypes.Integer)]
        public int? Split { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hours", modeltype: FwDataTypes.Decimal)]
        public decimal? Hours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursot", modeltype: FwDataTypes.Decimal)]
        public decimal? HoursOvertime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hoursdt", modeltype: FwDataTypes.Decimal)]
        public decimal? HoursDoubletime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewactualhours", modeltype: FwDataTypes.Boolean)]
        public bool? CrewActualHours { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterout", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "meterin", modeltype: FwDataTypes.Decimal)]
        public decimal? MeterIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "linetotalbeforedisc", modeltype: FwDataTypes.Decimal)]
        public decimal? LineTotalBeforeDiscount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.Decimal)]
        public decimal? Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "linetotal", modeltype: FwDataTypes.Decimal)]
        public decimal? LineTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "tax", modeltype: FwDataTypes.Decimal)]
        public decimal? Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "linetotalwithtax", modeltype: FwDataTypes.Decimal)]
        public decimal? LineTotalWithTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Decimal)]
        public decimal? Adjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rebateamount", modeltype: FwDataTypes.Decimal)]
        public decimal? RebateAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjrevenue", modeltype: FwDataTypes.Decimal)]
        public decimal? AdjustedRevenue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikpayextended", modeltype: FwDataTypes.Decimal)]
        public decimal? QuikPayExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "availfor", modeltype: FwDataTypes.Text)]
        public string AvailableFor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjusted", modeltype: FwDataTypes.Boolean)]
        public bool? IsAdjusted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isrecurring", modeltype: FwDataTypes.Boolean)]
        public bool? IsRecurring { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "manualbillflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsManualBill { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "voidinvoiceitemid", modeltype: FwDataTypes.Text)]
        public string VoidInvoiceItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprofitcenter", modeltype: FwDataTypes.Boolean)]
        public bool? IsProfitCenter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitcenterchg1", modeltype: FwDataTypes.Text)]
        public string ProfitCenterChargeCode1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitcenterchg2", modeltype: FwDataTypes.Text)]
        public string ProfitCenterChargeCode2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "profitcenterchg3", modeltype: FwDataTypes.Text)]
        public string ProfitCenterChargeCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activity", modeltype: FwDataTypes.Text)]
        public string Activity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityexportcode", modeltype: FwDataTypes.Text)]
        public string ActivityExportCode { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string invoiceId = GetUniqueIdAsString("InvoiceId", request) ?? "";
            string recType = GetUniqueIdAsString("RecType", request) ?? "";

            if (string.IsNullOrEmpty(invoiceId))
            {
                if (!string.IsNullOrEmpty(InvoiceItemId))
                {
                    string[] values = AppFunc.GetStringDataAsync(AppConfig, "invoiceitem", new string[] { "invoiceitemid" }, new string[] { InvoiceItemId }, new string[] { "invoiceid", "rectype" }).Result;
                    invoiceId = values[0];
                    recType = values[1];
                }
            }


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            select.AddParameter("@invoiceid", invoiceId);
            select.AddParameter("@rectype", recType);

            addFilterToSelect("AvailFor", "availfor", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}
