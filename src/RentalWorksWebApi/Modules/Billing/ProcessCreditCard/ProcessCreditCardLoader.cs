using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
namespace WebApi.Modules.Billing.ProcessCreditCard
{
    //[FwSqlTable("loadercte")]
    [FwSqlTable("processcreditcardloadview")]
    public class ProcessCreditCardLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ProcessCreditCardLoader()
        {
            //this.Cte.AppendLine("loadercte as (");
            //this.Cte.AppendLine("  select");
            //this.Cte.AppendLine("    title = 'Process Credit Card: ' + orderno,");
            //this.Cte.AppendLine("    pccv.*,");
            //this.Cte.AppendLine("    pinpad_code = 'XXXXXX', pinpad_description = 'XXXXXXXXXXX', pinpad_type = 'XXXXXXXXXXX',");
            //this.Cte.AppendLine("    totals_weekly_grosstotal = 123.45, totals_weekly_discount = 123.45, totals_weekly_subtotal = 123.45, totals_weekly_tax = 123.45, totals_weekly_grandtotal = 123.45,");
            //this.Cte.AppendLine("    totals_period_grosstotal = 123.45, totals_period_discount = 123.45, totals_period_subtotal = 123.45, totals_period_tax = 123.45, totals_period_grandtotal = 123.45,");
            //this.Cte.AppendLine("    totals_replacement_totalreplacementcost = 123.45, totals_replacement_depositpercentage = 10.1, totals_replacement_depositdue = 123.45,");
            //this.Cte.AppendLine("    payment_totalamount = 3423432.12, payment_deposit = 23423.2, payment_remainingamount = 230.23, payment_amounttopay = 0.00");
            //this.Cte.AppendLine("  from processcreditcardloadview pccv with (nolock)");
            //this.Cte.AppendLine(")");
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNo { get; set; }
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

        [FwSqlDataField(column: "depositper_centage", modeltype: FwDataTypes.Decimal)]
        public decimal Totals_Replacement_DepositDue { get { return Totals_Replacement_ReplacementCost * Totals_Replacement_DepositPercentage;  } }
        //------------------------------------------------------------------------------------ 


        // Payment Amount
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "payment_totalamount", modeltype: FwDataTypes.Decimal)]
        //public decimal Payment_TotalAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "payment_deposit", modeltype: FwDataTypes.Decimal)]
        //public decimal Payment_Deposit { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "payment_remainingamount", modeltype: FwDataTypes.Decimal)]
        //public decimal Payment_RemainingAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "payment_amounttopay", modeltype: FwDataTypes.Decimal)]
        //public decimal Payment_AmountToPay { get; set; }
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
