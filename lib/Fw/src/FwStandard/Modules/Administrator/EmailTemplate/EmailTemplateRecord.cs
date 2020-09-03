using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace FwStandard.Modules.Administrator.EmailTemplate
{
    [FwSqlTable("appemail")]
    public class EmailTemplateRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appemailid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string AppEmailId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FilterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subject", modeltype: FwDataTypes.Text, sqltype: "varchar")]
        public string Subject { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtext", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string EmailText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bodyformat", modeltype: FwDataTypes.Text, sqltype: "char")]
        public string BodyFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "emailtype", modeltype: FwDataTypes.Text, sqltype: "varchar")]
        public string EmailType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
