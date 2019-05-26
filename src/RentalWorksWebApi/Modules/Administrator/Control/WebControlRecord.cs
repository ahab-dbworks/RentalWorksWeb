using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Administrator.Control
{
    [FwSqlTable("webcontrol")]
    public class WebControlRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webcontrolid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ControlId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimageid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ReportLogoImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultContactGroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
    }
}