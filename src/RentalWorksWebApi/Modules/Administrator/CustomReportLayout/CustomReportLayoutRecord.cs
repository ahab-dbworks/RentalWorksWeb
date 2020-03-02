using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.CustomReportLayout
{
    [FwSqlTable("webreportlayout")]
    public class CustomReportLayoutRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webreportlayoutid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomReportLayoutId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "basereport", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: false)]
        public string BaseReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "html", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1, required: true)]
        public string Html { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "active", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assignto", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string AssignTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
