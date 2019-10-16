using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Billing.Invoice
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
        // GET api/v1/invoice/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "pBCVhSqQIGNnS")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Locked", RwGlobals.INVOICE_LOCKED_COLOR);
            legend.Add("No Charge", RwGlobals.INVOICE_NO_CHARGE_COLOR);
            legend.Add("Adjusted", RwGlobals.INVOICE_ADJUSTED_COLOR);
            legend.Add("Hiatus", RwGlobals.INVOICE_HIATUS_COLOR);
            legend.Add("Flat PO", RwGlobals.INVOICE_FLAT_PO_COLOR);
            legend.Add("Credit", RwGlobals.INVOICE_CREDIT_COLOR);
            legend.Add("Altered Dates", RwGlobals.INVOICE_ALTERED_DATES_COLOR);
            legend.Add("Repair", RwGlobals.INVOICE_REPAIR_COLOR);
            legend.Add("Estimate", RwGlobals.INVOICE_ESTIMATE_COLOR);
            legend.Add("Loss & Damage", RwGlobals.INVOICE_LOSS_AND_DAMAGE_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
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
        public async Task<ActionResult<IEnumerable<Invoice.InvoiceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
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
        //    return await <InvoiceLogic>DoDeleteAsync(id);
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
        // POST api/v1/invoice/creditinvoice
        [HttpPost("creditinvoice")]
        [FwControllerMethod(Id: "zs0EWzzJYFMop")]
        public async Task<ActionResult<CreditInvoiceReponse>> CreditInvoice([FromBody]CreditInvoiceRequest request)  
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InvoiceLogic l = new InvoiceLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.InvoiceId = request.InvoiceId;
                if (await l.LoadAsync<InvoiceLogic>())
                {
                    CreditInvoiceReponse response = await l.CreditInvoice(request); 
                    if (response.success)
                    {
                        if (!string.IsNullOrEmpty(response.CreditId))
                        {
                            response.credit = new InvoiceLogic();
                            response.credit.SetDependencies(AppConfig, UserSession);
                            response.credit.InvoiceId = response.CreditId;
                            bool b = await response.credit.LoadAsync<InvoiceLogic>();
                        }

                    }
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
