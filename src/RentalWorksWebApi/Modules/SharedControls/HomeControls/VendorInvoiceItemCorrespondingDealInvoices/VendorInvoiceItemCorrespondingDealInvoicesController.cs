using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.HomeControls.VendorInvoiceItemCorrespondingDealInvoices
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "billing-v1")]
    [FwController(Id: "06qm2k830e7D")]
    public class VendorInvoiceItemCorrespondingDealInvoicesController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public VendorInvoiceItemCorrespondingDealInvoicesController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceItemCorrespondingDealInvoicesLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitemcorrespondingdealinvoices/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "197o5q7f5LQ6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceitemcorrespondingdealinvoices/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "1BvbxatZ6t4v", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody] BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
