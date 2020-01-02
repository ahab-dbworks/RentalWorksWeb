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
                ReceiptBatchExportLoader l = new ReceiptBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.DoLoad<ReceiptBatchExportLoader>(request);

                string exportString = await AppFunc.GetStringDataAsync(AppConfig, "webdataexportformat", "dataexportformatid", request.DataExportFormatId, "exportstring");
                //request must contain a DataExportFormatId (provided from the field on the requesting Page)
                //we need a generic method here that will take an instance of a AppExportLoader ("l" in this scope) and an Export Format String (pulled from the provided DataExportFormatId)
                //   the method should use handlebars to produce a giant string from the "l" data object and the desired export format.
                //   the method should then produce a text file with that giant string and download it back to the page.


                AppExportResponse response = await Export<ReceiptBatchExportLoader>(l, exportString);

                // populate this field with the download file path and name
                //for now, the actual filename of the download file is not critical. But going forward we may need to add a field somewhere for user to define how they want the file named

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
    }
}
