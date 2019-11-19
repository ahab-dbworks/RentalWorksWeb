using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.AccountingSettings.AccountingSettings
{
    [FwLogic(Id: "wiM02oPASbPis")]
    public class AccountingSettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AccountingSettingsRecord accountingSettings = new AccountingSettingsRecord();
        AccountingSettingsLoader accountingSettingsLoader = new AccountingSettingsLoader();
        public AccountingSettingsLogic()
        {
            dataRecords.Add(accountingSettings);
            dataLoader = accountingSettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "wd8bATwZg023X", IsPrimaryKey: true)]
        public string ControlId { get { return accountingSettings.ControlId; } set { accountingSettings.ControlId = value; } }
        [FwLogicProperty(Id: "10OrwrySHB7C6", IsReadOnly: true)]
        public string Name { get; set; }
        [FwLogicProperty(Id: "Ip6xy5l1qxSZE")]
        public bool? UsePrefixOnAssetAccounts { get { return accountingSettings.UsePrefixOnAssetAccounts; } set { accountingSettings.UsePrefixOnAssetAccounts = value; } }
        [FwLogicProperty(Id: "pdGmIJcNoa1PY")]
        public bool? UsePrefixOnIncomeAccounts { get { return accountingSettings.UsePrefixOnIncomeAccounts; } set { accountingSettings.UsePrefixOnIncomeAccounts = value; } }
        [FwLogicProperty(Id: "Sb6YHcVOGHOsH")]
        public bool? UsePrefixOnExpenseAccounts { get { return accountingSettings.UsePrefixOnExpenseAccounts; } set { accountingSettings.UsePrefixOnExpenseAccounts = value; } }
        [FwLogicProperty(Id: "c0XoJP00xjFoO")]
        public bool? UsePrefixOnLiabilityAccounts { get { return accountingSettings.UsePrefixOnLiabilityAccounts; } set { accountingSettings.UsePrefixOnLiabilityAccounts = value; } }
        [FwLogicProperty(Id: "12nRmcD2y5qXs")]
        public bool? UseSuffixOnAssetAccounts { get { return accountingSettings.UseSuffixOnAssetAccounts; } set { accountingSettings.UseSuffixOnAssetAccounts = value; } }
        [FwLogicProperty(Id: "n6a2hHEijBEh9")]
        public bool? UseSuffixOnExpenseAccounts { get { return accountingSettings.UseSuffixOnExpenseAccounts; } set { accountingSettings.UseSuffixOnExpenseAccounts = value; } }
        [FwLogicProperty(Id: "Mcwv7CYXP3o2C")]
        public bool? UseSuffixOnIncomeAccounts { get { return accountingSettings.UseSuffixOnIncomeAccounts; } set { accountingSettings.UseSuffixOnIncomeAccounts = value; } }
        [FwLogicProperty(Id: "Mm6cchN3wow1M")]
        public bool? UseSuffixOnLiabilityAccounts { get { return accountingSettings.UseSuffixOnLiabilityAccounts; } set { accountingSettings.UseSuffixOnLiabilityAccounts = value; } }
        [FwLogicProperty(Id: "ysaoucS4Flkoy")]
        public decimal? AssetUnitCostThreshold { get { return accountingSettings.AssetUnitCostThreshold; } set { accountingSettings.AssetUnitCostThreshold = value; } }
        [FwLogicProperty(Id: "66CXCXCUwov30")]
        public bool? PurchaseUseCompleteKitAccounts { get { return accountingSettings.PurchaseUseCompleteKitAccounts; } set { accountingSettings.PurchaseUseCompleteKitAccounts = value; } }
        [FwLogicProperty(Id: "B8OG7Ysg8bdsd")]
        public bool? EnableForeignSubRentalWithholding { get { return accountingSettings.EnableForeignSubRentalWithholding; } set { accountingSettings.EnableForeignSubRentalWithholding = value; } }
        [FwLogicProperty(Id: "zf5VElgsDCtfZ")]
        public string ForeignSubRentalWithholdingCountryId { get { return accountingSettings.ForeignSubRentalWithholdingCountryId; } set { accountingSettings.ForeignSubRentalWithholdingCountryId = value; } }
        [FwLogicProperty(Id: "lzHzsEMxTErel", IsReadOnly: true)]
        public string ForeignSubRentalWithholdingCountry { get; set; }
        [FwLogicProperty(Id: "U01YcezwXAX6G")]
        public decimal? ForeignSubRentalWithholdingPercent { get { return accountingSettings.ForeignSubRentalWithholdingPercent; } set { accountingSettings.ForeignSubRentalWithholdingPercent = value; } }
        [FwLogicProperty(Id: "DTdOwEPyst0Vt")]
        public string DateStamp { get { return accountingSettings.DateStamp; } set { accountingSettings.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
