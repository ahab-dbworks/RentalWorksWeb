using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.GlAccount
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
    }
}
