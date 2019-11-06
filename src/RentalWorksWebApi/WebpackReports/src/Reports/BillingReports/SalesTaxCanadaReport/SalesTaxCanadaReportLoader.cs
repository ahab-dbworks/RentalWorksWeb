using WebApi.Modules.Reports.Shared.SalesTaxReport;
using WebLibrary;

namespace WebApi.Modules.Reports.Billing.SalesTaxCanadaReport
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
