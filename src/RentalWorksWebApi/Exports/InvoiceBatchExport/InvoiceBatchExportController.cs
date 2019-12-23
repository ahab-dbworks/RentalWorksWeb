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

                //request must contain a DataExportFormatId (provided from the field on the Page)
                //we need a generic method here that will take an instance of a AppExportLoader ("dt" in this scope) and an Export String (pulled from the provided DataExportFormatId)
                //   the method should use handlebars to produce a giant string from the data object and the desired export format.
                //   the method should then produce a text file with that giant string and download it back to the page.

                //for now, the actual filename of the download file is not critical. But going forward we may need to add a field somewhere for user to define how they want the file named


                return new OkObjectResult(dt);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
    }
}
