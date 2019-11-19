using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.VendorInvoiceProcessBatch
{
    public class VendorInvoiceProcessBatchRequest
    {
    }

    public class VendorInvoiceProcessBatchResponse : TSpStatusResponse
    {
        public string BatchId { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "gRjYvLD2qZ6NR")]
    public class VendorInvoiceProcessBatchController : AppDataController
    {
        public VendorInvoiceProcessBatchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceProcessBatchLogic); }

        // POST api/v1/vendorinvoiceprocessbatch/createbatch
        [HttpPost("createbatch")]
        [FwControllerMethod(Id: "puxaS6Kno8j", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "1EOaJNWMzCJJu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoiceprocessbatch/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "iKNPRwx4E2uA5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
