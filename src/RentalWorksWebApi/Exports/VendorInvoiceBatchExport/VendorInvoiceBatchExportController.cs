using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Exports.VendorInvoiceBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "kEKk799BSXVF")]
    public class VendorInvoiceBatchExportController : AppExportController
    {
        public  VendorInvoiceBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(VendorInvoiceBatchExportLoader); }

        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoicebatchexport/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "P3pnEhmIFtV4", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<VendorInvoiceBatchExportResponse>> Export([FromBody]VendorInvoiceBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VendorInvoiceBatchExportLoader l = new VendorInvoiceBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.DoLoad<VendorInvoiceBatchExportLoader>(request);

                string exportString = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", "dataexportformatid", request.DataExportFormatId, "exportstring");
                string downloadFileName = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", "dataexportformatid", request.DataExportFormatId, "filename");
                AppExportResponse response = await Export<VendorInvoiceBatchExportLoader>(l, exportString, downloadFileName);

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
    }
}
