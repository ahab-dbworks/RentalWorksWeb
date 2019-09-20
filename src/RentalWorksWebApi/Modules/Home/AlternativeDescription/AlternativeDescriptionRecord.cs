using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.AlternativeDescription
{
    [FwSqlTable("masteraka")]
    public class AlternativeDescriptionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterakaid", modeltype: FwDataTypes.Integer, sqltype: "int", identity:true, isPrimaryKey: true)]
        public int? AlternativeDescriptionId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? InternalChar { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aka", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string AKA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowedoninvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AllowedOnInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
