using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealClassification
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
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
