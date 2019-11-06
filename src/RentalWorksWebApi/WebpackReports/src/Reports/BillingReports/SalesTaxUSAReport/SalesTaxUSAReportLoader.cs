using WebApi.Modules.Reports.Shared.SalesTaxReport;
using WebLibrary;

namespace WebApi.Modules.Reports.Billing.SalesTaxUSAReport
{
    public class SalesTaxUSAReportLoader : SalesTaxReportLoader
    {
        //------------------------------------------------------------------------------------ 
        public SalesTaxUSAReportLoader()
        {
            TaxCountryFilter = RwConstants.TAX_COUNTRY_USA;
        }
        //------------------------------------------------------------------------------------ 
    }
}
