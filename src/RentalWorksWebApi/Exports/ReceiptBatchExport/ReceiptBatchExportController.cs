using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Exports.ReceiptBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "di8ahTdcwt0H")]
    public class ReceiptBatchExportController : AppExportController
    {
        public ReceiptBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReceiptBatchExportLoader); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptbatchexport/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "nqyMIjXrvytA", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<ReceiptBatchExportResponse>> Export([FromBody]ReceiptBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptBatchExportLoader data = new ReceiptBatchExportLoader();
                data.SetDependencies(this.AppConfig, this.UserSession);
                await data.DoLoad<ReceiptBatchExportLoader>(request);
                string[] exportSettings = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", new string[] { "dataexportformatid" }, new string[] { request.DataExportFormatId }, new string[] { "exportstring", "filename" });
                AppExportResponse response = Export<ReceiptBatchExportLoader>(data, exportSettings[0], exportSettings[1]);
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
