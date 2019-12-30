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
                InvoiceBatchExportLoader l = new InvoiceBatchExportLoader();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.DoLoad<InvoiceBatchExportLoader>(request);

                //request must contain a DataExportFormatId (provided from the field on the requesting Page)
                //we need a generic method here that will take an instance of a AppExportLoader ("l" in this scope) and an Export Format String (pulled from the provided DataExportFormatId)
                //   the method should use handlebars to produce a giant string from the "l" data object and the desired export format.
                //   the method should then produce a text file with that giant string and download it back to the page.

                /*
                 * 
                 * COPIED FROM InvoiceProcessBatchFunc.cs
                 * 
                // here we are creating a downloadable file that will live in the API "downloads" directory
                string downloadFileName = batch.BatchNumber + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
                string filename = userSession.WebUsersId + "_" + batch.BatchNumber + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + "_csv";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                using (var tw = new StreamWriter(path, false)) // false here will initialize the file fresh, no appending
                {
                    tw.Write(sb);
                    tw.Flush();
                    tw.Close();
                }


                response.batch = batch;
                response.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}";


                */




                InvoiceBatchExportResponse response = new InvoiceBatchExportResponse();
                response.downloadUrl = "";  // populate this field with the download file path and name
                                            //for now, the actual filename of the download file is not critical. But going forward we may need to add a field somewhere for user to define how they want the file named

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
