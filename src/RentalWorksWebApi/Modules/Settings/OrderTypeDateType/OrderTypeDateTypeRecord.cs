using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderTypeDateType
{
    [FwSqlTable("ordertypedatetype")]
    public class OrderTypeDateTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypedatetypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypeDateTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemtype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SystemType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptionrename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string DescriptionRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enabled", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Enabled { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredquote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RequiredOnQuote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? RequiredOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "milestone", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Milestone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productionactivity", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ProductionActivity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}