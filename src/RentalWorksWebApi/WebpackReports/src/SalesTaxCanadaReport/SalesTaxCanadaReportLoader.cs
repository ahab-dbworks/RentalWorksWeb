using WebLibrary;
using WebApi.Modules.Reports.SalesTaxReport;

namespace WebApi.Modules.Reports.SalesTaxCanadaReport
{
    public class SalesTaxCanadaReportLoader : SalesTaxReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesTaxCanadaReportLoader()
        {
            TaxCountryFilter = RwConstants.TAX_COUNTRY_CANADA;
        }
        //------------------------------------------------------------------------------------ 
    }
}
