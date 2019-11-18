using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Home.InventoryPrep
{
    [FwSqlTable("masterprep")]
    public class InventoryPrepRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterprepid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryPrepId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preprateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PrepRateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preprate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? PrepRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsDefault { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}