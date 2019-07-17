using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Administrator.CustomField
{
    [FwSqlTable("customfieldview")]
    public class CustomFieldLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomFieldId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customfieldname", modeltype: FwDataTypes.Text)]
        public string CustomFieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customtablename", modeltype: FwDataTypes.Text)]
        public string CustomTableName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname", modeltype: FwDataTypes.Text)]
        public string FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldtype", modeltype: FwDataTypes.Text)]
        public string FieldType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controltype", modeltype: FwDataTypes.Text)]
        public string ControlType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stringlength", modeltype: FwDataTypes.Integer)]
        public int? StringLength { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floatdecimaldigits", modeltype: FwDataTypes.Integer)]
        public int? FloatDecimalDigits { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "showinbrowse", modeltype: FwDataTypes.Boolean)]
        //public bool? ShowInBrowse { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "browsesizepx", modeltype: FwDataTypes.Integer)]
        //public int? BrowseSizeInPixels { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
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
