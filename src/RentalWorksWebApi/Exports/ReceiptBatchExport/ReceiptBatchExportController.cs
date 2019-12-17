using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Exports.ReceiptBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "di8ahTdcwt0H")]
    public class ReceiptBatchExportController : AppExportController
    {
        public ReceiptBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(ReceiptBatchExportLoader); }

        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptbatchexport/load
        [HttpPost("load")]
        [FwControllerMethod(Id: "qRewcKVuOGxi", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<ReceiptBatchExportLoader>> Load([FromBody]ReceiptBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptBatchExportLoader l = new ReceiptBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                ReceiptBatchExportLoader dt = await l.DoLoad<ReceiptBatchExportLoader>(request);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
    }
}
