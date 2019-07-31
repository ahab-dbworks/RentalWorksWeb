using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.AccountingSettings
{
    [FwSqlTable("glcontrol")]
    public class AccountingSettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true, sqltype: "char", maxlength: 8)]
        public string ControlId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "assetunitcost", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? AssetUnitCostThreshold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonasset", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UsePrefixOnAssetAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonexpense", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UsePrefixOnExpenseAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonincome", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UsePrefixOnIncomeAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefixonliability", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UsePrefixOnLiabilityAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseusepackagegls", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PurchaseUseCompleteKitAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonasset", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSuffixOnAssetAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonexpense", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSuffixOnExpenseAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonincome", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSuffixOnIncomeAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suffixonliability", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSuffixOnLiabilityAccounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enableforeignsrwithholding", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? EnableForeignSubRentalWithholding { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "foreignsrwithholdingcountryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ForeignSubRentalWithholdingCountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "foreignsrwithholdingpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? ForeignSubRentalWithholdingPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
