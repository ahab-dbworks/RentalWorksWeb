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

namespace WebApi.Modules.Home.ReceiptProcessBatch
{
    public class ReceiptProcessBatchRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }


    public class ReceiptProcessBatchResponse : TSpStatusReponse
    {
        public ReceiptProcessBatchLogic Batch;
    }

    public class ExportReceiptRequest
    {
        public string BatchId { get; set; }
    }

    public class ExportReceiptResponse : TSpStatusReponse
    {
        public ReceiptProcessBatchLogic batch = null;
        public string downloadUrl = "";
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "ThKpggGlj1hqd")]
    public class ReceiptProcessBatchController : AppDataController
    {
        public ReceiptProcessBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ReceiptProcessBatchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptprocessbatch/export
        [HttpPost("export")]
        [FwControllerMethod(Id: "qJl9ySG3Dc4")]
        public async Task<ActionResult<ExportReceiptResponse>> Export([FromBody]ExportReceiptRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptProcessBatchLogic batch = new ReceiptProcessBatchLogic();
                batch.SetDependencies(AppConfig, UserSession);
                batch.BatchId = request.BatchId;
                if (await batch.LoadAsync<ReceiptProcessBatchLogic>())
                {
                    ExportReceiptResponse response = await ReceiptProcessBatchFunc.Export(AppConfig, UserSession, batch);
                    return response;
                }
                else
                {
                    return NotFound();
                }
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
        // POST api/v1/receiptprocessbatch/createbatch
        [HttpPost("createbatch")]
        [FwControllerMethod(Id: "G7wMcVCbWQm")]
        public async Task<ActionResult<ReceiptProcessBatchResponse>> CreateBatch([FromBody]ReceiptProcessBatchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReceiptProcessBatchResponse response = await ReceiptProcessBatchFunc.CreateBatch(AppConfig, UserSession, request);
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
        // POST api/v1/receiptprocessbatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "v95ShkNjcztCo")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/receiptprocessbatch/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "QVqljKwnnW5j0")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
