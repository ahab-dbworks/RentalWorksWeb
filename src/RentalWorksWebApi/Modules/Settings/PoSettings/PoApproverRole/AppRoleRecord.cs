using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.PoSettings.AppRole
{
    [FwSqlTable("approle")]
    public class AppRoleRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "approleid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string AppRoleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "approle", modeltype: FwDataTypes.Text, maxlength: 40, required: true)]
        public string AppRole { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "roletype", modeltype: FwDataTypes.Text, maxlength: 40)]
        public string RoleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovertype", modeltype: FwDataTypes.Text, maxlength: 1, required: true)]
        public string PoApproverType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
