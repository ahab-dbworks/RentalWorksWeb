using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Home.InventorySubstitute
{
    [FwSqlTable("substitute")]
    public class InventorySubstituteRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mastersubstituteid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventorySubstituteId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "substituteid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string SubstituteInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}