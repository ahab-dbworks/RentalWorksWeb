using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.GLDistribution
{
    [FwLogic(Id: "rYvsDEoouvOa")]
    public class GLDistributionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GLDistributionLoader gLDistributionLoader = new GLDistributionLoader();
        public GLDistributionLogic()
        {
            dataLoader = gLDistributionLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "tOJjJzWdoliL", IsReadOnly: true)]
        public string Date { get; set; }
        [FwLogicProperty(Id: "4AxV3zyyMJaB", IsReadOnly: true)]
        public string GlAccountNo { get; set; }
        [FwLogicProperty(Id: "9MiWpOuowUoC", IsReadOnly: true)]
        public string GlAccountDescription { get; set; }
        [FwLogicProperty(Id: "NVyjjoOM8o74", IsReadOnly: true)]
        public decimal? Debit { get; set; }
        [FwLogicProperty(Id: "jRLC3bu7qxl4", IsReadOnly: true)]
        public decimal? Credit { get; set; }
        [FwLogicProperty(Id: "skn2ldiYeSfO", IsReadOnly: true)]
        public string GlAccountId { get; set; }
        [FwLogicProperty(Id: "amrIDBSIO8UH", IsReadOnly: true)]
        public string GroupHeading { get; set; }
        [FwLogicProperty(Id: "GB6WxY2SoCv2", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        [FwLogicProperty(Id: "49nj5XXWGU3q", IsReadOnly: true)]
        public int? GroupHeadingOrder { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
