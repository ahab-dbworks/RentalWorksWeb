using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Administrator.DuplicateRule
{
    [FwSqlTable("duplicateruleview")]
    public class DuplicateRuleLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duplicateruleid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DuplicateRuleId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string Modulename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rulename", modeltype: FwDataTypes.Text)]
        public string Rulename { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "casesensitive", modeltype: FwDataTypes.Boolean)]
        public bool Casesensitive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fields", modeltype: FwDataTypes.Text)]
        public string Fields { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}