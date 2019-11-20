using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.AddressSettings.State
{
    [FwSqlTable("state")]
    public class StateRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stateid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string StateId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statecode", modeltype: FwDataTypes.Text, maxlength: 2, required: true)]
        public string StateCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
