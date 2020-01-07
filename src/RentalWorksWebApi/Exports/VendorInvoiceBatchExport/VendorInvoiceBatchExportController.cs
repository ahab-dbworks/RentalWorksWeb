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
                VendorInvoiceBatchExportLoader data = new VendorInvoiceBatchExportLoader();
                data.SetDependencies(this.AppConfig, this.UserSession);
                await data.DoLoad<VendorInvoiceBatchExportLoader>(request);
                string[] exportSettings = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", new string[] { "dataexportformatid" }, new string[] { request.DataExportFormatId }, new string[] { "exportstring", "filename" });
                AppExportResponse response = Export<VendorInvoiceBatchExportLoader>(data, exportSettings[0], exportSettings[1]);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
