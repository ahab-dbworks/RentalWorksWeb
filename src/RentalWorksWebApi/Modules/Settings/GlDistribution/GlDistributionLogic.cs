using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.GlDistribution
{
    public class GlDistributionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GlDistributionRecord glDistribution = new GlDistributionRecord();
        GlDistributionLoader glDistributionLoader = new GlDistributionLoader();
        public GlDistributionLogic()
        {
            dataRecords.Add(glDistribution);
            dataLoader = glDistributionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GlDistributionId { get { return glDistribution.GlDistributionId; } set { glDistribution.GlDistributionId = value; } }
        public string GlAccountId { get { return glDistribution.GlAccountId; } set { glDistribution.GlAccountId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string AccountType { get { return glDistribution.AccountType; } set { glDistribution.AccountType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AccountTypeDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GlAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GlAccountDescription { get; set; }
        public string DateStamp { get { return glDistribution.DateStamp; } set { glDistribution.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}