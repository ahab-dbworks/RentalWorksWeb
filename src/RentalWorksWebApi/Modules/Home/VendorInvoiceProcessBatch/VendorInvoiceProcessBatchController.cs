using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using FwStandard.AppManager;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.VendorInvoiceProcessBatch
{
    public class VendorInvoiceProcessBatchRequest
    {
    }

    public class VendorInvoiceProcessBatchResponse : TSpStatusReponse
    {
        public string BatchId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "gRjYvLD2qZ6NR")]
    public class VendorInvoiceProcessBatchController : AppDataController
    {
        public VendorInvoiceProcessBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceProcessBatchLogic); }

        // POST api/v1/vendorinvoiceprocessbatch/createbatch
        [HttpPost("createbatch")]
        [FwControllerMethod(Id: "puxaS6Kno8j")]
        public async Task<ActionResult<VendorInvoiceProcessBatchResponse>> CreateBatch([FromBody]VendorInvoiceProcessBatchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                VendorInvoiceProcessBatchResponse response = await VendorInvoiceProcessBatchFunc.CreateBatch(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceprocessbatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "1EOaJNWMzCJJu")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceprocessbatch/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "iKNPRwx4E2uA5")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
