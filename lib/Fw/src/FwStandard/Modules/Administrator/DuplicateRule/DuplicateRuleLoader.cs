using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
//using WebApi.Data; 
//using System.Collections.Generic;
namespace FwStandard.Modules.Administrator.DuplicateRule
{
    [FwSqlTable("duplicateruleview")]
    public class DuplicateRuleLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duplicateruleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DuplicateRuleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rulename", modeltype: FwDataTypes.Text)]
        public string RuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "casesensitive", modeltype: FwDataTypes.Boolean)]
        public bool? CaseSensitive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemrule", modeltype: FwDataTypes.Boolean)]
        public bool? SystemRule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fields", modeltype: FwDataTypes.Text)]
        public string Fields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldtypes", modeltype: FwDataTypes.Text)]
        public string FieldTypes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rulenamecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RuleNameColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "considerblanks", modeltype: FwDataTypes.Boolean)]
        public bool? ConsiderBlanks { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}