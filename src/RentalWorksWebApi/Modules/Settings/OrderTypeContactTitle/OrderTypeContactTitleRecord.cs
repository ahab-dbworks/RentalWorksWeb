using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderTypeContactTitle
{
    [FwSqlTable("ordertypecontacttitle")]
    public class OrderTypeContactTitleRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypecontacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeContactTitleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}