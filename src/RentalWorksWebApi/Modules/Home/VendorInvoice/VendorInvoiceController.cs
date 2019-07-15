using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.VendorInvoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"Fq9aOe0yWfY")]
    public class VendorInvoiceController : AppDataController
    {
        public VendorInvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"nRYgEuIU5vz")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"t8MkQaaK0jc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoice 
        [HttpGet]
        [FwControllerMethod(Id:"bHw4Sqclw45")]
        public async Task<ActionResult<IEnumerable<VendorInvoiceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoice/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GNbOCZjjNmu")]
        public async Task<ActionResult<VendorInvoiceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice 
        [HttpPost]
        [FwControllerMethod(Id:"NeM52Pf9TK6")]
        public async Task<ActionResult<VendorInvoiceLogic>> PostAsync([FromBody]VendorInvoiceLogic l)
        {
            return await DoPostAsync<VendorInvoiceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoice/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"tm4l8YxIUK5")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/toggleapproved/A0000001
        [HttpPost("toggleapproved/{id}")]
        [FwControllerMethod(Id: "qGQ28sAtqVz4")]
        public async Task<ActionResult<ToggleVendorInvoiceApprovedResponse>> ToggleApproved([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                VendorInvoiceLogic l = new VendorInvoiceLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<VendorInvoiceLogic>(ids))
                {
                    ToggleVendorInvoiceApprovedResponse response = await l.ToggleApproved();
                    return new OkObjectResult(response);
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
    }
}
