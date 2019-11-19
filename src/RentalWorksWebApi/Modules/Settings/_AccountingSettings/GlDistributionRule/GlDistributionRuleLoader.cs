using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.AccountingSettings.GlDistributionRule
{
    [FwSqlTable("gldistributionview")]
    public class GlDistributionRuleLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldistributionid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GlDistributionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text)]
        public string GlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accounttype", modeltype: FwDataTypes.Text)]
        public string AccountType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accounttypedesc", modeltype: FwDataTypes.Text)]
        public string AccountTypeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text)]
        public string GlAccountDescription { get; set; }
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