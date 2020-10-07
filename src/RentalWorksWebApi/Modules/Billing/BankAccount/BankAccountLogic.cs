using WebApi.Logic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Home.BankAccount
{
    [FwLogic(Id: "xNv1wTFLxsDXn")]
    public class BankAccountLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BankAccountRecord bankAccount = new BankAccountRecord();
        BankAccountLoader bankAccountLoader = new BankAccountLoader();
        public BankAccountLogic()
        {
            dataRecords.Add(bankAccount);
            dataLoader = bankAccountLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "xOD2Bb5p4NDko", IsPrimaryKey: true)]
        public int? BankAccountId { get { return bankAccount.BankAccountId; } set { bankAccount.BankAccountId = value; } }
        [FwLogicProperty(Id: "XODxXclqGlT22", IsRecordTitle: true)]
        public string AccountName { get { return bankAccount.AccountName; } set { bankAccount.AccountName = value; } }
        [FwLogicProperty(Id: "xpCEc8HboxilG")]
        public string CheckNumber { get { return bankAccount.CheckNumber; } set { bankAccount.CheckNumber = value; } }
        [FwLogicProperty(Id: "xPH7atpPxg1GY")]
        public string OfficeLocationId { get { return bankAccount.OfficeLocationId; } set { bankAccount.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "XpXzxVAak7c5B", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "XQC5l7Vu7I1Mj")]
        public string CurrencyId { get { return bankAccount.CurrencyId; } set { bankAccount.CurrencyId = value; } }
        [FwLogicProperty(Id: "XREJ6Yc0cq1jY", IsReadOnly: true)]
        public string Currency { get; set; }
        [FwLogicProperty(Id: "xSfhkgSpb74Cz", IsReadOnly: true)]
        public string CurrencyCode { get; set; }
        [FwLogicProperty(Id: "XsgfE1HUrmY2P", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }
        [FwLogicProperty(Id: "XSGkJCMQLwxxA")]
        public bool? Inactive { get { return bankAccount.Inactive; } set { bankAccount.Inactive = value; } }
        [FwLogicProperty(Id: "xSGPbo4VSmjkz")]
        public string DateStamp { get { return bankAccount.DateStamp; } set { bankAccount.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            //prevent user from changing currency if payments/transactions exist

            if (isValid)
            {
                if (saveMode.Equals(TDataRecordSaveMode.smUpdate))
                {
                    if ((CurrencyId != null) && (!CurrencyId.Equals(((BankAccountLogic)original).CurrencyId)))
                    {
                        if (AppFunc.DataExistsAsync(AppConfig, "payment", new string[] { "accountid" }, new string[] { BankAccountId.ToString() }).Result)
                        {
                            isValid = false;
                            validateMsg = $"Cannot change the Currency for this {BusinessLogicModuleName} because payments/transactions exist.";
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}
