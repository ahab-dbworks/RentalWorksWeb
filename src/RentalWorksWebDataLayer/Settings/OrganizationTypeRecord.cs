using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("organizationtype")]
    public class OrganizationTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "organizationtypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string OrganizationTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "organizationtype", dataType: FwDataTypes.Text, length: 40)]
        public string OrganizationType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "code", dataType: FwDataTypes.Text, length: 5)]
        public string OrganizationTypeCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactiveflg", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
