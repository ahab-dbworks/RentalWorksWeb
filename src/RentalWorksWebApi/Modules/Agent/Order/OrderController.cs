using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Agent.Quote;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Settings.CurrencySettings.Currency;
using WebApi.Modules.Settings.DepartmentLocation;
using WebApi.Modules.Settings.DocumentSettings.CoverLetter;
using WebApi.Modules.Settings.DocumentSettings.TermsConditions;
using WebApi.Modules.Settings.MarketSegmentJob;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.OrderSettings.DiscountReason;
using WebApi.Modules.Settings.OrderSettings.MarketSegment;
using WebApi.Modules.Settings.OrderSettings.MarketType;
using WebApi.Modules.Settings.OrderSettings.OrderType;
using WebApi.Modules.Settings.OrderTypeLocation;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Settings.ShipViaSettings.ShipVia;
using WebApi.Modules.Settings.TaxSettings.TaxOption;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using static WebApi.Modules.HomeControls.DealOrder.DealOrderRecord;

namespace WebApi.Modules.Agent.Order
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "U8Zlahz3ke9i")]
    [FwOptionsGroup("Copy to Quote / Order", "7pJLfUY1U01T")]
    [FwOptionsGroup("Cancel / Uncancel", "cSxghAONeqcu")]
    public partial class OrderController : AppDataController
    {
        public OrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "UPMWQbWfEcnn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/order/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "TbaR3uXNguGT", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("On Hold", RwGlobals.QUOTE_ORDER_ON_HOLD_COLOR);
            legend.Add("No Charge", RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR);
            legend.Add("Late", RwGlobals.ORDER_LATE_COLOR);
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            legend.Add("Multi-Warehouse", RwGlobals.QUOTE_ORDER_MULTI_WAREHOUSE_COLOR);
            legend.Add("Repair", RwGlobals.ORDER_REPAIR_COLOR);
            legend.Add("Loss & Damage", RwGlobals.ORDER_LOSS_AND_DAMAGE_COLOR);
            //legend.Add("Unassigned Subs", RwGlobals.SUB_COLOR);  // commented to avoid confusion until this enhancement is completed
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "5ceamSTDb0ao", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/copytemplate
        [HttpPost("copytemplate")]
        [FwControllerMethod(Id: "ABZ5hvc8H5P", ActionType: FwControllerActionTypes.Edit, Caption: "Copy Template")]
        public async Task<ActionResult<CopyTemplateResponse>> CopyTemplate([FromBody] CopyTemplateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CopyTemplateResponse response = await OrderFunc.CopyTemplateAsync(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/copytoquote/A0000001
        [HttpPost("copytoquote/{id}")]
        [FwControllerMethod(Id: "I5BjXCxr9HqC", ActionType: FwControllerActionTypes.Option, Caption: "Copy to Quote", ParentId: "7pJLfUY1U01T")]
        public async Task<ActionResult<QuoteLogic>> CopyToQuote([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic l = new OrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    QuoteLogic lCopy = await l.CopyToQuoteAsync<OrderBaseLogic>(copyRequest);
                    return new OkObjectResult(lCopy);
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
        // POST api/v1/order/copytoorder/A0000001
        [HttpPost("copytoorder/{id}")]
        [FwControllerMethod(Id: "AEtE2boE2vxW", ActionType: FwControllerActionTypes.Option, Caption: "Copy to Order", ParentId: "7pJLfUY1U01T")]
        public async Task<ActionResult<OrderLogic>> CopyToOrder([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic l = new OrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    OrderLogic lCopy = await l.CopyToOrderAsync<OrderBaseLogic>(copyRequest);
                    return new OkObjectResult(lCopy);
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
        // POST api/v1/order/copyorderitems
        [HttpPost("copyorderitems")]
        [FwControllerMethod(Id: "i11GB44Ddvm", ActionType: FwControllerActionTypes.Option, Caption: "Copy Order Items", ParentId: "7pJLfUY1U01T")]
        public async Task<ActionResult<CopyOrderItemsResponse>> CopyOrderItems([FromBody]CopyOrderItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CopyOrderItemsResponse response = await OrderFunc.CopyOrderItems(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/cancel/A0000001
        [HttpPost("cancel/{id}")]
        [FwControllerMethod(Id: "b8eSiRATn80I", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Order", ParentId: "cSxghAONeqcu")]
        public async Task<ActionResult<OrderLogic>> CancelOrder([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic Order = new OrderLogic();
                Order.SetDependencies(AppConfig, UserSession);
                if (await Order.LoadAsync<OrderLogic>(ids))
                {
                    await Order.CancelOrderASync();
                    return new OkObjectResult(Order);
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
        // POST api/v1/order/uncancel/A0000001
        [HttpPost("uncancel/{id}")]
        [FwControllerMethod(Id: "XDBoHNmP6jzE", ActionType: FwControllerActionTypes.Option, Caption: "Uncancel Order", ParentId: "cSxghAONeqcu")]
        public async Task<ActionResult<OrderLogic>> UncancelOrder([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic Order = new OrderLogic();
                Order.SetDependencies(AppConfig, UserSession);
                if (await Order.LoadAsync<OrderLogic>(ids))
                {
                    await Order.UncancelOrderASync();
                    return new OkObjectResult(Order);
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
        // POST api/v1/quote/createsnapshot/A0000001
        [HttpPost("createsnapshot/{id}")]
        [FwControllerMethod(Id: "q7jExMRUzP6G", ActionType: FwControllerActionTypes.Option, Caption: "Create Snapshot")]
        public async Task<ActionResult<OrderLogic>> CreateSnapshot([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic order = new OrderLogic();
                order.SetDependencies(AppConfig, UserSession);
                if (await order.LoadAsync<OrderLogic>(ids))
                {
                    OrderLogic newVersion = await order.CreateSnapshotASync();
                    return new OkObjectResult(newVersion);
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
        // POST api/v1/order/onhold/A0000001
        [HttpPost("onhold/{id}")]
        [FwControllerMethod(Id: "ChTLbGO95bgpJ", ActionType: FwControllerActionTypes.Option, Caption: "Put On Hold / Remove Hold")]
        public async Task<ActionResult<OrderOnHoldResponse>> OnHoldOrder([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderOnHoldResponse response = new OrderOnHoldResponse();
                string[] ids = id.Split('~');
                OrderLogic Order = new OrderLogic();
                Order.SetDependencies(AppConfig, UserSession);
                if (await Order.LoadAsync<OrderLogic>(ids))
                {
                    response = await Order.OnHoldOrderASync();
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
        // POST api/v1/order/applybottomlinedaysperweek
        [HttpPost("applybottomlinedaysperweek")]
        [FwControllerMethod(Id: "VUTqN1ZvB7l7", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Days Per Week")]
        public async Task<ActionResult<bool>> ApplyBottomLineDaysPerWeek([FromBody] ApplyBottomLineDaysPerWeekRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.OrderId };

                OrderLogic l = new OrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineDaysPerWeek(request);
                    return new OkObjectResult(true);
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
        // POST api/v1/order/applybottomlinediscountpercent
        [HttpPost("applybottomlinediscountpercent")]
        [FwControllerMethod(Id: "3unCMsILlPMW", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Discount Percent")]
        public async Task<ActionResult<bool>> ApplyBottomLineDiscountPercent([FromBody] ApplyBottomLineDiscountPercentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.OrderId };

                OrderLogic l = new OrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineDiscountPercent(request);
                    return new OkObjectResult(true);
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
        // POST api/v1/order/applybottomlinetotal
        [HttpPost("applybottomlinetotal")]
        [FwControllerMethod(Id: "UPTvT6dmADf9", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Total")]
        public async Task<ActionResult<bool>> ApplyBottomLineTotal([FromBody] ApplyBottomLineTotalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = new string[] { request.OrderId };

                OrderLogic l = new OrderLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<OrderLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineTotal(request);
                    return new OkObjectResult(true);
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
        // POST api/v1/order/startcreatepoworksheetsession
        [HttpPost("startcreatepoworksheetsession")]
        [FwControllerMethod(Id: "R0PUscxxQIGy", ActionType: FwControllerActionTypes.Edit, Caption: "Start Create PO Worksheet Session")]
        public async Task<ActionResult<CreatePoWorksheetSessionResponse>> StartCreatePoWorksheetSession([FromBody] CreatePoWorksheetSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CreatePoWorksheetSessionResponse response = new CreatePoWorksheetSessionResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.RateType))
                {
                    response.success = false;
                    response.msg = "RateType is required.";
                }
                else if (string.IsNullOrEmpty(request.BillingCycleId))
                {
                    response.success = false;
                    response.msg = "Billing Cycle is required.";
                }
                else if (request.FromDate == null)
                {
                    response.success = false;
                    response.msg = "From Date is required.";
                }
                else
                {
                    response = await OrderFunc.StartCreatePoWorksheetSession(AppConfig, UserSession, request);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/order/startmodifypoworksheetsession
        [HttpPost("startmodifypoworksheetsession")]
        [FwControllerMethod(Id: "LxXovTHWdtaz0", ActionType: FwControllerActionTypes.Edit, Caption: "Start Modify PO Worksheet Session")]
        public async Task<ActionResult<ModifyPoWorksheetSessionResponse>> StartModifyPoWorksheetSession([FromBody] ModifyPoWorksheetSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ModifyPoWorksheetSessionResponse response = new ModifyPoWorksheetSessionResponse();
                if (string.IsNullOrEmpty(request.OrderId))
                {
                    response.success = false;
                    response.msg = "OrderId is required.";
                }
                else if (string.IsNullOrEmpty(request.PurchaseOrderId))
                {
                    response.success = false;
                    response.msg = "PurchaseOrderId is required.";
                }
                else
                {
                    response = await OrderFunc.StartModifyPoWorksheetSession(AppConfig, UserSession, request);
                }

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // GET api/v1/order/poworksheetsessiontotals
        [HttpGet("poworksheetsessiontotals/{sessionId}")]
        [FwControllerMethod(Id: "prgSOVMTbnMEw", ActionType: FwControllerActionTypes.Edit, Caption: "Get PO Worksheet Session Totals")]
        public async Task<ActionResult<PoWorksheetSessionTotalsResponse>> GetPoWorksheetSessionTotals([FromRoute] string sessionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PoWorksheetSessionTotalsResponse response = await OrderFunc.GetPoWorksheetSessionTotals(AppConfig, UserSession, sessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------          
        // POST api/v1/order/getcustomrates/
        [HttpPost("getcustomrates")]
        [FwControllerMethod(Id: "hL2AY72hx75T", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GetCustomRatesResponse>> GetCustomRates([FromBody] GetCustomRatesRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GetCustomRatesResponse response = await OrderFunc.GetCustomRates(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        

        // POST api/v1/order/completepoworksheetsession
        [HttpPost("completepoworksheetsession")]
        [FwControllerMethod(Id: "D2UdqmBoUarE", ActionType: FwControllerActionTypes.Edit, Caption: "Complete PO Worksheet Session")]
        public async Task<ActionResult<CompletePoWorksheetSessionResponse>> CompletePoWorksheetSession([FromBody] CompletePoWorksheetSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CompletePoWorksheetSessionResponse response = await OrderFunc.CompletePoWorksheetSession(AppConfig, UserSession, request.SessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/order/changeofficelocation/A0000001
        [HttpPost("changeofficelocation/{id}")]
        [FwControllerMethod(Id: "xFLAlAgAGKG07", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ChangeOrderOfficeLocationResponse>> ChangeOfficeLocation([FromRoute]string id, [FromBody] ChangeOrderOfficeLocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                OrderLogic Order = new OrderLogic();
                Order.SetDependencies(AppConfig, UserSession);
                if (await Order.LoadAsync<OrderLogic>(ids))
                {
                    ChangeOrderOfficeLocationResponse response = await Order.ChangeOfficeLocationASync(request);
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
        // GET api/v1/order/A0000001/messages
        [HttpGet("{id}/messages")]
        [FwControllerMethod(Id: "EkH24Uo3xLhBX", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<OrderMessagesResponse>> GetOrderMessages([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OrderMessagesRequest request = new OrderMessagesRequest();
                request.OrderId = id;
                OrderMessagesResponse response = await OrderFunc.GetOrderMessages(AppConfig, UserSession, request);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
        // GET api/v1/order
        [HttpGet]
        [FwControllerMethod(Id: "proTzh4yd7gn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "Va6jzxcGVll8", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        [FwControllerMethod(Id: "iK5RRY2gSg8d", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrderLogic>> NewAsync([FromBody]OrderLogic l)
        {
            return await DoNewAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/orde/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "GqYmAEOhhzrUQ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrderLogic>> EditAsync([FromRoute] string id, [FromBody]OrderLogic l)
        {
            return await DoEditAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "OdDdFEOtDAQm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "RvgyeeTmnc6J", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateratetype/browse 
        [HttpPost("validateratetype/browse")]
        [FwControllerMethod(Id: "Lhmop3rLsRkM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateordertype/browse 
        [HttpPost("validateordertype/browse")]
        [FwControllerMethod(Id: "GUJBwI20pPTP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateagent/browse 
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "ptOI0ELLh3Cy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateprojectmanager/browse 
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "sBdJje4alE1u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateoutsidesalesrepresentative/browse 
        [HttpPost("validateoutsidesalesrepresentative/browse")]
        [FwControllerMethod(Id: "EsYOkgv6yG86", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutsideSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatemarkettype/browse 
        [HttpPost("validatemarkettype/browse")]
        [FwControllerMethod(Id: "oSUWfIENsmj5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatemarketsegment/browse 
        [HttpPost("validatemarketsegment/browse")]
        [FwControllerMethod(Id: "EsQlUTFTBudH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketSegmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketSegmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatemarketsegmentjob/browse 
        [HttpPost("validatemarketsegmentjob/browse")]
        [FwControllerMethod(Id: "O7BaHG9VChvA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketSegmentJobBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketSegmentJobLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatecoverletter/browse 
        [HttpPost("validatecoverletter/browse")]
        [FwControllerMethod(Id: "tg4K9obsl4l2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCoverLetterBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CoverLetterLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatetermsconditions/browse 
        [HttpPost("validatetermsconditions/browse")]
        [FwControllerMethod(Id: "zmkNapF4Oohh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTermsConditionsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TermsConditionsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatebillingcycle/browse 
        [HttpPost("validatebillingcycle/browse")]
        [FwControllerMethod(Id: "hEfVmxCx6r0N", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatepaymentterms/browse 
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "Y4TntVyUXJLw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatepaymenttype/browse 
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "vvVoLBUf9WDF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatecurrency/browse 
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "jpfVBV5hvGYf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatetaxoption/browse 
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "FtK3YxuI2TB1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOptionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatediscountreason/browse 
        [HttpPost("validatediscountreason/browse")]
        [FwControllerMethod(Id: "LpVEGTObAodd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDiscountReasonBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DiscountReasonLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateissuedtocountry/browse 
        [HttpPost("validateissuedtocountry/browse")]
        [FwControllerMethod(Id: "PLtbNEaXGPSt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIssuedToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateoutdeliverycarrier/browse 
        [HttpPost("validateoutdeliverycarrier/browse")]
        [FwControllerMethod(Id: "12rLD5NyXpkn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateoutdeliveryshipvia/browse 
        [HttpPost("validateoutdeliveryshipvia/browse")]
        [FwControllerMethod(Id: "aAKK2WOrpHnn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateindeliverycarrier/browse 
        [HttpPost("validateindeliverycarrier/browse")]
        [FwControllerMethod(Id: "es4xTMu5GCNO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateindeliveryshipvia/browse 
        [HttpPost("validateindeliveryshipvia/browse")]
        [FwControllerMethod(Id: "DcIFJPpBBfan", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateoutdeliverytocountry/browse 
        [HttpPost("validateoutdeliverytocountry/browse")]
        [FwControllerMethod(Id: "3z6SaBdtOdHk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateindeliverytocountry/browse 
        [HttpPost("validateindeliverytocountry/browse")]
        [FwControllerMethod(Id: "aqOk4pQHW6hY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "plQJsVV8rREu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "QL5eyv9AFxEu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validatebilltocountry/browse 
        [HttpPost("validatebilltocountry/browse")]
        [FwControllerMethod(Id: "41DHh6BQqeru", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/order/department/{departmentid}/location/{locationid}
        [HttpGet("department/{departmentid}/location/{locationid}")]
        [FwControllerMethod(Id: "eJ5FTKoL04UY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DepartmentLocationLogic>> DepartmentLocation_GetOneAsync([FromRoute]string departmentid, [FromRoute]string locationid)
        {
            return await DoGetAsync<DepartmentLocationLogic>($"{departmentid}~{locationid}", typeof(DepartmentLocationLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/order/ordertype/A0000001 
        [HttpGet("ordertype/{ordertypeid}")]
        [FwControllerMethod(Id: "Vd0qk8YeseP8", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrderTypeLogic>> OrderType_GetOneAsync([FromRoute]string ordertypeid)
        {
            return await DoGetAsync<OrderTypeLogic>(ordertypeid, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/ordertypelocation/browse 
        [HttpPost("ordertypelocation/browse")]
        [FwControllerMethod(Id: "ISu3pJ6XJNTd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> OrderTypeLocation_BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderTypeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
