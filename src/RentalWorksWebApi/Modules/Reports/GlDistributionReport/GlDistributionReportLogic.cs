using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.GlDistributionReport
{
    public class GlDistributionReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GlDistributionReportLoader glDistributionReportLoader = new GlDistributionReportLoader();
        public GlDistributionReportLogic()
        {
            dataLoader = glDistributionReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        public string LocationId { get; set; }
        public string Location { get; set; }
        public string GroupHeading { get; set; }
        public int? GroupHeadingOrder { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
    }
}
