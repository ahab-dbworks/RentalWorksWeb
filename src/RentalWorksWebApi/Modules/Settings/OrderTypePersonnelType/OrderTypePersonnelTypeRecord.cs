using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.OrderTypePersonnelType
{
    [FwSqlTable("ordertypepersonneltype")]
    public class OrderTypePersonnelTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypepersonneltypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string OrderTypePersonnelTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required:true)]
        public string OrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personneltypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PersonnelTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "personneltype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string PersonnelTypeRename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showofficephone", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowOfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showofficeext", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowOfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "showcellular", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ShowCellular { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 1)]
        public decimal? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}