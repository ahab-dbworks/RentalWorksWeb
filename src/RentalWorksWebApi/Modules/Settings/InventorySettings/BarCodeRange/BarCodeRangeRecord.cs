using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.InventorySettings.BarCodeRange
{
    [FwSqlTable("barcoderange")]
    public class BarCodeRangeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcoderangeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string BarCodeRangeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefix", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2)]
        public string Prefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodefrom", modeltype: FwDataTypes.Integer, sqltype: "numeric", required: true)]
        public int? BarcodeFrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeto", modeltype: FwDataTypes.Integer, sqltype: "numeric", required: true)]
        public int? BarcodeTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}