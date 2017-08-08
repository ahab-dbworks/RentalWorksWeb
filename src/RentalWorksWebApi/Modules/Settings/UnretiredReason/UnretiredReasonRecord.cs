using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.UnretiredReason
{
    [FwSqlTable("unretiredreason")]
    public class UnretiredReasonRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unretiredreasonid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string UnretiredReasonId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unretiredreason", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string UnretiredReason { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "reasontype", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string ReasonType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
