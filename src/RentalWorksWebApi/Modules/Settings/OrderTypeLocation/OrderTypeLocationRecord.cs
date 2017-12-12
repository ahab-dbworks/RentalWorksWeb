using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderTypeLocation
{
    [FwSqlTable("ordertypelocation")]
    public class OrderTypeLocationRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypelocationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeLocationId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TermsConditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "coverletterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CoverLetterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceclass", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string InvoiceClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}