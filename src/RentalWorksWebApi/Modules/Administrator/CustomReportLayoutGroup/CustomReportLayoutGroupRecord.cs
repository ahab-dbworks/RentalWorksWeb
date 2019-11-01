using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.CustomReportLayoutGroup
{
    [FwSqlTable("webreportlayoutgroups")]
    public class CustomReportLayoutGroupRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webreportlayoutgroupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomReportLayoutGroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webreportlayoutid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomReportLayoutId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
