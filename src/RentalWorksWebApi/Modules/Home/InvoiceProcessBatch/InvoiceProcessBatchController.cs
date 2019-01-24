using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.InvoiceProcessBatch
{
    public class InvoiceProcessBatchRequest
    {
        public string LocationId { get; set; }
        public DateTime AsOfDate { get; set; }
    }

    public class InvoiceProcessBatchResponse : TSpStatusReponse
    {
        public string BatchId { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "I8d2wTNNRmRJa")]
    public class InvoiceProcessBatchController : AppDataController
    {
        public InvoiceProcessBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceProcessBatchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceprocessbatch/createbatch
        [HttpPost("createbatch")]
        [FwControllerMethod(Id: "jFW8SvpVrWd")]
        public async Task<ActionResult<InvoiceProcessBatchResponse>> CreateBatch([FromBody]InvoiceProcessBatchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceProcessBatchResponse response = await InvoiceProcessBatchFunc.CreateBatch(AppConfig, UserSession, request);
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
        // POST api/v1/invoiceprocessbatch/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "uMULFcgBt8Z3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoiceprocessbatch/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "5c7daJCHS7cyI")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
