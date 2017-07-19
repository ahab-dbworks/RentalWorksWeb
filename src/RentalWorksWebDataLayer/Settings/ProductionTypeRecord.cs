using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("prodtype")]
    public class ProductionTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "prodtypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string ProductionTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "prodtype", dataType: FwDataTypes.Text, length: 20)]
        public string ProductionType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "prodcode", dataType: FwDataTypes.Text, length: 10)]
        public string ProductionTypeCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
