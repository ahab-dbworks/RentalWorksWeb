using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Exports.InvoiceBatchExport
{
    [Route("api/v1/[controller]")]
    //[ApiExplorerSettings(GroupName = "reports-v1")]
    [FwController(Id: "GI2FxKtrjja1")]
    public class InvoiceBatchExportController : AppExportController
    {
        public InvoiceBatchExportController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { loaderType = typeof(InvoiceBatchExportLoader); }

        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoicebatchexport/load
        [HttpPost("load")]
        [FwControllerMethod(Id: "gfekPjE6qLe0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<InvoiceBatchExportLoader>> Load([FromBody]InvoiceBatchExportRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceBatchExportLoader l = new InvoiceBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                InvoiceBatchExportLoader dt = await l.DoLoad<InvoiceBatchExportLoader>(request);
                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
    }
}
