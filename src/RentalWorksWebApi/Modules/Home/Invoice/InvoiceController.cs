using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Home.Invoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "cZ9Z8aGEiDDw")]
    public class InvoiceController : AppDataController
    {
        public InvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "QHbwnxEN2Ud9")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoice/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "uWXaeVXku2ry")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoice 
        [HttpGet]
        [FwControllerMethod(Id: "IRyboGiUWku1")]
        public async Task<ActionResult<IEnumerable<InvoiceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InvoiceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoice/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "o6y4PTxCqILC")]
        public async Task<ActionResult<InvoiceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InvoiceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoice 
        [HttpPost]
        [FwControllerMethod(Id: "bgrJjmFPGAtE")]
        public async Task<ActionResult<InvoiceLogic>> PostAsync([FromBody]InvoiceLogic l)
        {
            return await DoPostAsync<InvoiceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/invoice/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"TPgslKxFR6")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
        // POST api/v1/invoice/A0000001/void
        [HttpPost("{id}/void")]
        [FwControllerMethod(Id: "xEo3YJ6FHSYE")]
        public async Task<ActionResult<InvoiceLogic>> Void([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                InvoiceLogic l = new InvoiceLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<InvoiceLogic>(ids))
                {
                    TSpStatusResponse response = await l.Void();
                    if (response.success)
                    {
                        await l.LoadAsync<InvoiceLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------     
        // POST api/v1/invoice/A0000001/approve
        [HttpPost("{id}/approve")]
        [FwControllerMethod(Id: "1OiRex9QtrM")]
        public async Task<ActionResult<ToggleInvoiceApprovedResponse>> Approve([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                InvoiceLogic l = new InvoiceLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<InvoiceLogic>(ids))
                {
                    ToggleInvoiceApprovedResponse response = await l.Approve();
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------     
        // POST api/v1/invoice/A0000001/unapprove
        [HttpPost("{id}/unapprove")]
        [FwControllerMethod(Id: "cbkHowiSy8and")]
        public async Task<ActionResult<ToggleInvoiceApprovedResponse>> Unapprove([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                InvoiceLogic l = new InvoiceLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<InvoiceLogic>(ids))
                {
                    ToggleInvoiceApprovedResponse response = await l.Unapprove();
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------     
    }
}
