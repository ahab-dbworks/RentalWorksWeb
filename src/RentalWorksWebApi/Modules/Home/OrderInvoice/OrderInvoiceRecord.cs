using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.OrderInvoice
{
    [FwSqlTable("orderinvoice")]
    public class OrderInvoiceRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderInvoiceId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 25)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 4, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billableflat", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsBillableFlatPoInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "supplementalpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SupplementalFlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdiscountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OrderDiscountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectmanagerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ProjectManagerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicesubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? OrderInvoiceSubtotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicetax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? OrderInvoiceTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderinvoicetotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? OrderInvoiceTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? RentalSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salessubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? SalesSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacesubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? FacilitiesSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborsubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LaborSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscsubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? MiscellaneousSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationsubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? TransportationSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldsubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LossAndDamageSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsalesubtotal", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? RentalSaleSubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? RentalTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? SalesTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LaborTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misctax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? MiscellaneousTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldtax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? LossAndDamageTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalsaletax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? RentalSaleTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? FacilitiesTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationtax", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? TransportationTax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "premiumpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 16, scale: 10)]
        public decimal? PremiumPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AgentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billedhiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsBilledHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingend", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string BillingEndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingstart", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string BillingStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billperiodevent", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string BillingCycleEvent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billschedid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string EpisodeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludefromflat", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsExcludeFromFlatPo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "flatpoid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FlatPoId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatusdisc", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? HiatusDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
