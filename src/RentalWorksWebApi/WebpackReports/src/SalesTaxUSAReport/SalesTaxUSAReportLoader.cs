using WebLibrary;
using WebApi.Modules.Reports.SalesTaxReport;

namespace WebApi.Modules.Reports.SalesTaxUSAReport
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
