using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.AccountingSettings
{
    [FwSqlTable("glcontrolview")]
    public class AccountingSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ControlId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonasset", modeltype: FwDataTypes.Boolean)]
        public bool? UsePrefixOnAssetAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonincome", modeltype: FwDataTypes.Boolean)]
        public bool? UsePrefixOnIncomeAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonexpense", modeltype: FwDataTypes.Boolean)]
        public bool? UsePrefixOnExpenseAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonliability", modeltype: FwDataTypes.Boolean)]
        public bool? UsePrefixOnLiabilityAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonasset", modeltype: FwDataTypes.Boolean)]
        public bool? UseSuffixOnAssetAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonexpense", modeltype: FwDataTypes.Boolean)]
        public bool? UseSuffixOnExpenseAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonincome", modeltype: FwDataTypes.Boolean)]
        public bool? UseSuffixOnIncomeAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonliability", modeltype: FwDataTypes.Boolean)]
        public bool? UseSuffixOnLiabilityAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assetunitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? AssetUnitCostThreshold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseusepackagegls", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseUseCompleteKitAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableforeignsrwithholding", modeltype: FwDataTypes.Boolean)]
        public bool? EnableForeignSubRentalWithholding { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "foreignsrwithholdingcountryid", modeltype: FwDataTypes.Text)]
        public string ForeignSubRentalWithholdingCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "foreignsrwithholdingcountry", modeltype: FwDataTypes.Text)]
        public string ForeignSubRentalWithholdingCountry { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "foreignsrwithholdingpct", modeltype: FwDataTypes.Decimal)]
        public decimal? ForeignSubRentalWithholdingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
