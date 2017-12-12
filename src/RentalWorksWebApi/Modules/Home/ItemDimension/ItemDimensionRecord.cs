using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Home.ItemDimension
{
    [FwSqlTable("itemdimension")]
    public class ItemDimensionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid", isPrimaryKey: true, modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightlbs", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ShipWeightLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightoz", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ShipWeightOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightwcaselbs", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WeightInCaseLbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightwcaseoz", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WeightInCaseOz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthft", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthin", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WidthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightft", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? HeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightin", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? HeightIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthft", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthin", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LengthIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightkg", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ShipWeightKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipweightg", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? ShipWeightG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightwcasekg", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WeightInCaseKg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightwcaseg", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WeightInCaseG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WidthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthcm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? WidthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? HeightM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightcm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? HeightCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LengthM { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthcm", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? LengthCm { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}