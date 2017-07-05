using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebDataLayer.Settings;
using System;

namespace RentalWorksWebLogic.Settings
{
    public class GlAccountLogic : RwBusinessLogic
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
        public string GlAccountNo { get { return glAccount.GlAccountNo; } set { glAccount.GlAccountNo = value; } }
        public string GlAccountDescription { get { return glAccount.GlAccountDescription; } set { glAccount.GlAccountDescription = value; } }
        public string GlAccountType { get { return glAccount.GlAccountType; } set { glAccount.GlAccountType = value; } }
        public string Inactive { get { return glAccount.Inactive; } set { glAccount.Inactive = value; } }
        public DateTime? DateStamp { get { return glAccount.DateStamp; } set { glAccount.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
