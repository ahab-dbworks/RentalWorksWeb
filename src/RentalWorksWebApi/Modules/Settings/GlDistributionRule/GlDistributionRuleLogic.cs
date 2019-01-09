using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.GlDistributionRule
{
    [FwLogic(Id:"VaIT9Ka9gJHt")]
    public class GlDistributionRuleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GlDistributionRuleRecord glDistribution = new GlDistributionRuleRecord();
        GlDistributionRuleLoader glDistributionLoader = new GlDistributionRuleLoader();
        public GlDistributionRuleLogic()
        {
            dataRecords.Add(glDistribution);
            dataLoader = glDistributionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"gB6b84UbQN3T", IsPrimaryKey:true)]
        public string GlDistributionId { get { return glDistribution.GlDistributionId; } set { glDistribution.GlDistributionId = value; } }

        [FwLogicProperty(Id:"X4NySyopNIE")]
        public string GlAccountId { get { return glDistribution.GlAccountId; } set { glDistribution.GlAccountId = value; } }

        [FwLogicProperty(Id:"aziVE4bQbLH5", IsRecordTitle:true)]
        public string AccountType { get { return glDistribution.AccountType; } set { glDistribution.AccountType = value; } }

        [FwLogicProperty(Id:"aziVE4bQbLH5", IsReadOnly:true)]
        public string AccountTypeDescription { get; set; }

        [FwLogicProperty(Id:"lMTgXGtlhd6I", IsReadOnly:true)]
        public string GlAccountNo { get; set; }

        [FwLogicProperty(Id:"0bNWUaGvwjw5", IsReadOnly:true)]
        public string GlAccountDescription { get; set; }

        [FwLogicProperty(Id:"MGV3xUcPbdB")]
        public string DateStamp { get { return glDistribution.DateStamp; } set { glDistribution.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
