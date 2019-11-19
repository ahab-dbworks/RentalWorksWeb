using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Settings.AccountingSettings.GlAccount
{
    [FwLogic(Id:"rWByCGXkw3aP")]
    public class GlAccountLogic : AppBusinessLogic
    {
        GlAccountRecord glAccount = new GlAccountRecord();
        //------------------------------------------------------------------------------------
        public GlAccountLogic()
        {
            dataRecords.Add(glAccount);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"kdz9DS159hlA", IsPrimaryKey:true)]
        public string GlAccountId { get { return glAccount.GlAccountId; } set { glAccount.GlAccountId = value;} }

        [FwLogicProperty(Id:"jfSv55mM7Vug", IsRecordTitle:true)]
        public string GlAccountNo { get { return glAccount.GlAccountNo; } set { glAccount.GlAccountNo = value; } }

        [FwLogicProperty(Id:"cLlHk6CbLsN")]
        public string GlAccountDescription { get { return glAccount.GlAccountDescription; } set { glAccount.GlAccountDescription = value; } }

        [FwLogicProperty(Id:"gtASGI18iNX")]
        public string GlAccountType { get { return glAccount.GlAccountType; } set { glAccount.GlAccountType = value; } }

        [FwLogicProperty(Id:"xWB1rOnrGvi")]
        public bool? Inactive { get { return glAccount.Inactive; } set { glAccount.Inactive = value; } }

        [FwLogicProperty(Id:"QWRfPyjCK2a")]
        public string DateStamp { get { return glAccount.DateStamp; } set { glAccount.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                //PropertyInfo property = typeof(ReceiptLogic).GetProperty(nameof(ReceiptLogic.PaymentBy));
                PropertyInfo property = GetType().GetProperty(nameof(GlAccountLogic.GlAccountType));
                string[] acceptableValues = { RwConstants.GL_ACCOUNT_TYPE_ASSET, RwConstants.GL_ACCOUNT_TYPE_INCOME, RwConstants.GL_ACCOUNT_TYPE_EXPENSE, RwConstants.GL_ACCOUNT_TYPE_LIABILITY };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}
