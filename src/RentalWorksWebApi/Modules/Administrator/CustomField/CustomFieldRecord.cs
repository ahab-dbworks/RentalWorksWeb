using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Administrator.CustomField
{
    [FwSqlTable("customfield")]
    public class CustomFieldRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CustomFieldId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customtablename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string CustomTableName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string CustomFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stringlength", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? StringLength { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floatdecimaldigits", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? FloatDecimalDigits { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "showinbrowse", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? ShowInBrowse { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "browsesizepx", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? BrowseSizeInPixels { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("ModuleName", "modulename", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}