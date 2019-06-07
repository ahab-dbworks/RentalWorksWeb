using FwStandard.AppManager;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebLibrary;
using WebApi.Modules.Home.Quote;
using FwStandard.SqlServer;
using static WebApi.Modules.Home.DealOrder.DealOrderRecord;

namespace WebApi.Modules.Home.Order
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "U8Zlahz3ke9i")]
    public class OrderController : AppDataController
    {
        public OrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "5ceamSTDb0ao")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/order/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "d3Q7wt3ufSHTb")]
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
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "5ceamSTDb0ao")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/copytemplate
        [HttpPost("copytemplate")]
        [FwControllerMethod(Id: "ABZ5hvc8H5P")]
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
        [FwControllerMethod(Id: "I5BjXCxr9HqC")]
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
        [FwControllerMethod(Id: "I5BjXCxr9HqC")]
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
        [FwControllerMethod(Id: "i11GB44Ddvm")]
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
        [FwControllerMethod(Id: "b8eSiRATn80I")]
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
        [FwControllerMethod(Id: "XDBoHNmP6jzE")]
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
        [FwControllerMethod(Id: "q7jExMRUzP6G")]
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
        [FwControllerMethod(Id: "ChTLbGO95bgpJ")]
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
        [FwControllerMethod(Id: "VUTqN1ZvB7l7")]
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
        [FwControllerMethod(Id: "3unCMsILlPMW")]
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
        [FwControllerMethod(Id: "UPTvT6dmADf9")]
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
        [FwControllerMethod(Id: "R0PUscxxQIGy")]
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
        [FwControllerMethod(Id: "LxXovTHWdtaz0")]
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
        [FwControllerMethod(Id: "prgSOVMTbnMEw")]
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


        // POST api/v1/order/completepoworksheetsession
        [HttpPost("completepoworksheetsession")]
        [FwControllerMethod(Id: "D2UdqmBoUarE")]
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
        // GET api/v1/order
        [HttpGet]
        [FwControllerMethod(Id: "proTzh4yd7gn")]
        public async Task<ActionResult<IEnumerable<OrderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "Va6jzxcGVll8")]
        public async Task<ActionResult<OrderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        [FwControllerMethod(Id: "iK5RRY2gSg8d")]
        public async Task<ActionResult<OrderLogic>> PostAsync([FromBody]OrderLogic l)
        {
            return await DoPostAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
    }
}
