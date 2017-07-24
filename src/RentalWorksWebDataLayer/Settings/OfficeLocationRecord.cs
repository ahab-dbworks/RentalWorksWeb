using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("location")]
    public class OfficeLocationRecord : RwDataReadWriteRecord
    {
        [FwSqlDataField(columnName: "loccode", dataType: FwDataTypes.Text, length: 4)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "location", dataType: FwDataTypes.Text, length: 30)]
        public string Location { get; set; } = "";
        //------------------------------------------------------------------------------------        
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }

    }
}
