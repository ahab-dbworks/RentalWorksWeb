using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Billing.Receipt
{
    [FwSqlTable("arwebview")]
    public class ReceiptLoader : ReceiptBrowseLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmttype", modeltype: FwDataTypes.Text)]
        public string PaymentTypeType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeexportpaymentmethod", modeltype: FwDataTypes.Text)]
        public string PaymentTypeExportPaymentMethod { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text)]
        public string ModifiedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modby", modeltype: FwDataTypes.Text)]
        public string ModifiedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text)]
        public string ChargeBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdepositid", modeltype: FwDataTypes.Text)]
        public string DealDepositId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdepositcheckno", modeltype: FwDataTypes.Text)]
        public string DealDepositCheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdepositid", modeltype: FwDataTypes.Text)]
        public string CustomerDepositId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdepositcheckno", modeltype: FwDataTypes.Text)]
        public string CustomerDepositCheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overpaymentid", modeltype: FwDataTypes.Text)]
        public string OverPaymentId { get; set; }
        //------------------------------------------------------------------------------------
    }
}
