using FwStandard.BusinessLogic;
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Administrator.DuplicateRuleField
{
    [FwSqlTable("duplicaterulefield")]
    public class DuplicateRuleFieldRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duplicaterulefieldid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DuplicateRuleFieldId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duplicateruleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DuplicateRuleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("DuplicateRuleId", "duplicateruleid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}