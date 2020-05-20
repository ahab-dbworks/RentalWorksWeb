using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.SystemSettings.DefaultSettings
{
    [FwSqlTable("controlview")]
    public class DefaultSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DefaultSettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'Default Settings'", modeltype: FwDataTypes.Text)]
        public string DefaultSettingsName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultunitid", modeltype: FwDataTypes.Text)]
        public string DefaultUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultunit", modeltype: FwDataTypes.Text)]
        public string DefaultUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text)]
        public string DefaultDealStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DefaultDealStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdealporeq", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultDealPoRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultdealpotype", modeltype: FwDataTypes.Text)]
        public string DefaultDealPoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatusid", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultbillperiodid", modeltype: FwDataTypes.Text)]
        public string DefaultDealBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealdefaultbillperiod", modeltype: FwDataTypes.Text)]
        public string DefaultDealBillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonrecurbillperiodid", modeltype: FwDataTypes.Text)]
        public string DefaultNonRecurringBillingCycleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonrecurbillperiod", modeltype: FwDataTypes.Text)]
        public string DefaultNonRecurringBillingCycle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefaultpaytermsid", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerPaymentTermsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerdefaultpayterms", modeltype: FwDataTypes.Text)]
        public string DefaultCustomerPaymentTerms { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcontactgroupsid", modeltype: FwDataTypes.Text)]
        public string DefaultContactGroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultcontactgroupname", modeltype: FwDataTypes.Text)]
        public string DefaultContactGroupName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultrank", modeltype: FwDataTypes.Text)]
        public string DefaultRank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
        }
        //------------------------------------------------------------------------------------ 
    }
}
 