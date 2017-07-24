using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("department")]
    public class DepartmentRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "departmentid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string DepartmentId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "department", dataType: FwDataTypes.Text, length: 30)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "deptcode", dataType: FwDataTypes.Text, length: 20)]
        public string DepartmentCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "divisioncode", dataType: FwDataTypes.Text, length: 20)]
        public string DivisionCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}