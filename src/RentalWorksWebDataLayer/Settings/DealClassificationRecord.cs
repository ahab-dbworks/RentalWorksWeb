using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("dealclassification")]
    public class DealClassificationRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealclassificationid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string DealClassificationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "dealclassification", dataType: FwDataTypes.Text, length: 20)]
        public string DealClassification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
