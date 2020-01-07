using FwCore.Controllers;
using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "GI2FxKtrjja1")]
    public class InvoiceBatchExportController : AppExportController
    {
        public InvoiceBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InvoiceBatchExportLoader); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatchexport/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "gfekPjE6qLe0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<InvoiceBatchExportResponse>> Export([FromBody]InvoiceBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceBatchExportLoader data = new InvoiceBatchExportLoader();
                data.SetDependencies(this.AppConfig, this.UserSession);
                await data.DoLoad<InvoiceBatchExportLoader>(request);
                string[] exportSettings = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", new string[] { "dataexportformatid" }, new string[] { request.DataExportFormatId }, new string[] { "exportstring", "filename" });
                AppExportResponse response = Export<InvoiceBatchExportLoader>(data, exportSettings[0], exportSettings[1]);
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
