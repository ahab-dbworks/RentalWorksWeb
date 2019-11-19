using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.PoSettings.PoApproverRole
{
    [FwSqlTable("poapproverroleview")]
    public class PoApproverRoleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapproverroleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PoApproverRoleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapproverrole", modeltype: FwDataTypes.Text)]
        public string PoApproverRole { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "poapprovertype", modeltype: FwDataTypes.Text)]
        public string PoApproverType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "roletype", modeltype: FwDataTypes.Text)]
        public string RoleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
