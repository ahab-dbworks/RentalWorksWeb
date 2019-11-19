using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.AdministratorControls.CustomFormGroup
{
    [FwSqlTable("webformgroups")]
    public class CustomFormGroupRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webformgroupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomFormGroupId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webformid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CustomFormId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
