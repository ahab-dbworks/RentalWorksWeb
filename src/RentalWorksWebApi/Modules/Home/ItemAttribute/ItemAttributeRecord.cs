using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Home.ItemAttribute
{
    [FwSqlTable("itemattribute")]
    public class ItemAttributeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemattributeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ItemAttributeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AttributeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attributevalueid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AttributeValueId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numericvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 32, scale: 12)]
        public decimal? NumericValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}