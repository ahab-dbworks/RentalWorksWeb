using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Settings.TaxSettings.TaxOption;
using WebApi.Modules.Utilities.GLDistribution;

namespace WebApi.Modules.Billing.VendorInvoice
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"Fq9aOe0yWfY")]
    public class VendorInvoiceController : AppDataController
    {
        public VendorInvoiceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorInvoiceLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoice/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "rDWak7o2DDTHN", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"nRYgEuIU5vz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"t8MkQaaK0jc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoice 
        [HttpGet]
        [FwControllerMethod(Id:"bHw4Sqclw45", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorInvoiceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorInvoiceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/vendorinvoice/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"GNbOCZjjNmu", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VendorInvoiceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorInvoiceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice 
        [HttpPost]
        [FwControllerMethod(Id:"NeM52Pf9TK6", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorInvoiceLogic>> NewAsync([FromBody]VendorInvoiceLogic l)
        {
            return await DoNewAsync<VendorInvoiceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/vendorinvoice/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "CEVtssdgL5Ubf", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorInvoiceLogic>> EditAsync([FromRoute] string id, [FromBody]VendorInvoiceLogic l)
        {
            return await DoEditAsync<VendorInvoiceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/vendorinvoice/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"tm4l8YxIUK5", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorInvoiceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vendorinvoice/toggleapproved/A0000001
        [HttpPost("toggleapproved/{id}")]
        [FwControllerMethod(Id: "qGQ28sAtqVz4", ActionType: FwControllerActionTypes.Option, Caption: "Toggle Approved")]
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorinvoice/validatetaxoption/browse
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "oCPfYW0xPHzb", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/invoice/gldistribution/browse 
        [HttpPost("gldistribution/browse")]
        [FwControllerMethod(Id: "VKsYwE1DN0Gq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> GLDistribution_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GLDistributionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
