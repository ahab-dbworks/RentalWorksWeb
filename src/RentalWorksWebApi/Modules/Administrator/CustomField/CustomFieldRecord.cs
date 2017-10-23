using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Administration.CustomField
{
    [FwSqlTable("customfield")]
    public class CustomFieldRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomFieldId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customtablename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string CustomTableName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string CustomFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}