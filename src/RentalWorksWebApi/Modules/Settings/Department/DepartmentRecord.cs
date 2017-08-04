using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.Department
{
    [FwSqlTable("department")]
    public class DepartmentRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DepartmentId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "deptcode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string DepartmentCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "divisioncode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string DivisionCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}