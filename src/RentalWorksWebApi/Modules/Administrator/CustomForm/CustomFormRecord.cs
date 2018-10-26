using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.CustomForm
{
    [FwSqlTable("webform")]
    public class CustomFormRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webformid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomFormId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "baseform", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: false)]
        public string BaseForm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "html", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1, required: true)]
        public string Html { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "active", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
