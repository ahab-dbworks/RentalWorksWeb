using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.PoClassification
{
    [FwSqlTable("poclassification")]
    public class PoClassificationRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "poclassificationid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string PoClassificationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "poclassification", dataType: FwDataTypes.Text, length: 40)]
        public string PoClassification { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "excludefromroa", dataType: FwDataTypes.Boolean)]
        public bool ExcludeFromRoa { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactiveflg", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
