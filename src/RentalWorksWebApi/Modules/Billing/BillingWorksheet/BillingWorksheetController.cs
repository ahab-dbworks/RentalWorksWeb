using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using System;
using WebApi.Modules.Billing.Invoice;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Settings.CurrencySettings.Currency;
using WebApi.Modules.Settings.TaxSettings.TaxOption;

namespace WebApi.Modules.Billing.BillingWorksheet
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "2BTZbIXJy4tdI")]
    public class BillingWorksheetController : AppDataController
    {
        public BillingWorksheetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingWorksheetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "2BuRM5RuI855b", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "2DhrshWnhf5qO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billingworksheet 
        [HttpGet]
        [FwControllerMethod(Id: "2dZcAVnpwsGxX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<BillingWorksheetLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BillingWorksheetLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billingworksheet/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "2ehNCdLgjTrpV", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<BillingWorksheetLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingWorksheetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet 
        [HttpPost]
        [FwControllerMethod(Id: "2eUvhnt0ta38x", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<BillingWorksheetLogic>> NewAsync([FromBody]BillingWorksheetLogic l)
        {
            return await DoNewAsync<BillingWorksheetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/billingworksheet/A00000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "f40W7HIzvg9AH", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<BillingWorksheetLogic>> EditAsync([FromBody]BillingWorksheetLogic l)
        {
            return await DoEditAsync<BillingWorksheetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/billingworksheet/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "2GF2QLKIto8hY", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BillingWorksheetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/invoice/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "vgVJlEENk5rgB", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
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
        ////------------------------------------------------------------------------------------     
        //// POST api/v1/invoice/A0000001/approve
        [HttpPost("{id}/approve")]
        [FwControllerMethod(Id: "eMYtyUHlOkUuo", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ToggleInvoiceApprovedResponse>> Approve([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                BillingWorksheetLogic l = new BillingWorksheetLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<BillingWorksheetLogic>(ids))
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
        //// POST api/v1/invoice/A0000001/unapprove
        [HttpPost("{id}/unapprove")]
        [FwControllerMethod(Id: "sJydlcSDO02Zs", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<ToggleInvoiceApprovedResponse>> Unapprove([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                BillingWorksheetLogic l = new BillingWorksheetLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<BillingWorksheetLogic>(ids))
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
        // POST api/v1/billingworksheet/validateorder/browse
        [HttpPost("validateorder/browse")]
        [FwControllerMethod(Id: "xNlS681dUPWy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "N2H68D0rxllg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validateagent/browse
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "t7QKLmzvPa5O", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validateprojectmanager/browse
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "GdkBX708D3JE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validateoutsidesalesrepresentative/browse
        [HttpPost("validateoutsidesalesrepresentative/browse")]
        [FwControllerMethod(Id: "bCtJKTBnDcA8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutsideSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validatepaymentterms/browse
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "sqBJqBRM73z4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validatepaymenttype/browse
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "syDzukNzymIl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validatecurrency/browse
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "20U5IRkDNnlH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingworksheet/validatetaxoption/browse
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "GqSbm6Hhcyrm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOptionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
    }
}
