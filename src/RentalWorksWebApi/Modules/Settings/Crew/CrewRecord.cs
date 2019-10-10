using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.Crew
{
    [FwSqlTable("crew")]
    public class CrewRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, isPrimaryKey: true, sqltype: "char", maxlength: 8)]
        public string CrewId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractemployee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsContractEmployee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcrew", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsSubCrew { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
