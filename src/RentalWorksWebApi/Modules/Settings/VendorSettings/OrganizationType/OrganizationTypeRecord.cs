using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VendorSettings.OrganizationType
{
    [FwSqlTable("organizationtype")]
    public class OrganizationTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "organizationtypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string OrganizationTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "organizationtype", modeltype: FwDataTypes.Text, maxlength: 40, required: true)]
        public string OrganizationType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "code", modeltype: FwDataTypes.Text, maxlength: 5, required: true)]
        public string OrganizationTypeCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactiveflg", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
