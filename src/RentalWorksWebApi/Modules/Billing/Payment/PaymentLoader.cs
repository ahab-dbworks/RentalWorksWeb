using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.Payment
{
    [FwSqlTable("paymentwebview")]
    public class PaymentLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PaymentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentdate", modeltype: FwDataTypes.Date)]
        public string PaymentDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accountid", modeltype: FwDataTypes.Integer)]
        public int? BankAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "account", modeltype: FwDataTypes.Text)]
        public string AccountName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentdocno", modeltype: FwDataTypes.Text)]
        public string PaymentDocumentNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "amount", modeltype: FwDataTypes.Decimal)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedbyid", modeltype: FwDataTypes.Text)]
        public string AppliedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedby", modeltype: FwDataTypes.Text)]
        public string AppliedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text)]
        public string ModifiedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modby", modeltype: FwDataTypes.Text)]
        public string ModifiedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchid", modeltype: FwDataTypes.Text)]
        public string ChargeBatchId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string ChargeBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
