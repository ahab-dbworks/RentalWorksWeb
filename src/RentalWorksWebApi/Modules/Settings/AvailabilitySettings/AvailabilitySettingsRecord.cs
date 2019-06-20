using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.AvailabilitySettings
{
    [FwSqlTable("availabilitycontrol")]
    public class AvailabilitySettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, isPrimaryKey: true)]
        public string ControlId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "staleavailpollseconds", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? PollForStaleAvailabilitySeconds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "keepavailabilitycachecurrent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? KeepAvailabilityCacheCurrent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "keepcurrentseconds", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? KeepCurrentSeconds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
