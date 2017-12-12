using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.GlAccount
{
    public class GlAccountLogic : AppBusinessLogic
    {
        GlAccountRecord glAccount = new GlAccountRecord();
        //------------------------------------------------------------------------------------
        public GlAccountLogic()
        {
            dataRecords.Add(glAccount);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GlAccountId { get { return glAccount.GlAccountId; } set { glAccount.GlAccountId = value;} }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GlAccountNo { get { return glAccount.GlAccountNo; } set { glAccount.GlAccountNo = value; } }
        public string GlAccountDescription { get { return glAccount.GlAccountDescription; } set { glAccount.GlAccountDescription = value; } }
        public string GlAccountType { get { return glAccount.GlAccountType; } set { glAccount.GlAccountType = value; } }
        public bool? Inactive { get { return glAccount.Inactive; } set { glAccount.Inactive = value; } }
        public string DateStamp { get { return glAccount.DateStamp; } set { glAccount.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
