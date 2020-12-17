using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Billing.ProcessCreditCard
{
    [FwSqlTable("processcreditcardloadview")]
    public class ProcessCreditCardLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ProcessCreditCardLoader() : base()
        {

        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
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
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }

        // Weekly Totals
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_weekly_grosstotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Weekly_GrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_weekly_discount", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Weekly_Discount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_weekly_subtotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Weekly_SubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_weekly_tax", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Weekly_Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_weekly_grandtotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Weekly_GrandTotal { get; set; }

        // Period Totals
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_period_grosstotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Period_GrossTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_period_discount", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Period_Discount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_period_subtotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Period_SubTotal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_period_tax", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Period_Tax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_period_grandtotal", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Period_GrandTotal { get; set; }

        // Replacement Totals
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_replacement_replacementcost", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Replacement_ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totals_replacement_depositpercentage", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Replacement_DepositPercentage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string AgentBarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 


        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            
            // Get One
            if (request == null) {
                // filter by orderid
                select.AddWhere("orderid = @orderid");
                select.AddParameter("@orderid", this.OrderId);  
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
