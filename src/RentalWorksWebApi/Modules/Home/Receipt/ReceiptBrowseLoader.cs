using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Home.Receipt
{
    [FwSqlTable("arview")]
    public class ReceiptBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ReceiptId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerdeal", modeltype: FwDataTypes.Text)]
        public string CustomerDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedby", modeltype: FwDataTypes.Text)]
        public string AppliedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.Decimal)]
        public decimal? PaymentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string ChargeBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
