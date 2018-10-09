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
using WebApi.Logic;
using System.ComponentModel.DataAnnotations;
using FwStandard.SqlServer;

namespace WebApi.Modules.Home.Order
{


    public class CreatePoWorksheetSessionRequest
    {
        public string OrderId;
        public string RecType;
        public string VendorId;
        public string ContactId;
        public string RateType;
        public string BillingCycleId;
        public DateTime? RequiredDate;
        public string RequiredTime;
        public DateTime? FromDate;
        public DateTime? ToDate;
        public string DeliveryId;
        public bool? AdjustContractDates;
    }

    public class CreatePoWorksheetSessionResponse : TSpStatusReponse
    {
        public string SessionId;
    }


    public class CompletePoWorksheetSessionRequest
    {
        public string SessionId;
    }

    public class CompletePoWorksheetSessionResponse : TSpStatusReponse
    {
        public string PurchaseOrderId;
    }


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderController : AppDataController
    {
        public OrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/order/copy/A0000001
        [HttpPost("copy/{id}")]
        public async Task<IActionResult> Copy([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
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
                    if (copyRequest.CopyToType.Equals(RwConstants.ORDER_TYPE_QUOTE))
                    {
                        QuoteLogic lCopy = (QuoteLogic)await l.CopyAsync<OrderBaseLogic>(copyRequest);
                        return new OkObjectResult(lCopy);
                    }
                    else
                    {
                        OrderLogic lCopy = (OrderLogic)await l.CopyAsync<OrderBaseLogic>(copyRequest);
                        return new OkObjectResult(lCopy);
                    }
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
        // POST api/v1/order/cancel/A0000001
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelOrder([FromRoute]string id)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/order/uncancel/A0000001
        [HttpPost("uncancel/{id}")]
        public async Task<IActionResult> UncancelOrder([FromRoute]string id)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------     
        // POST api/v1/quote/createsnapshot/A0000001
        [HttpPost("createsnapshot/{id}")]
        public async Task<IActionResult> CreateSnapshot([FromRoute]string id)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/order/applybottomlinedaysperweek
        [HttpPost("applybottomlinedaysperweek")]
        public async Task<IActionResult> ApplyBottomLineDaysPerWeek([FromBody] ApplyBottomLineDaysPerWeekRequest request)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/applybottomlinediscountpercent
        [HttpPost("applybottomlinediscountpercent")]
        public async Task<IActionResult> ApplyBottomLineDiscountPercent([FromBody] ApplyBottomLineDiscountPercentRequest request)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/applybottomlinetotal
        [HttpPost("applybottomlinetotal")]
        public async Task<IActionResult> ApplyBottomLineTotal([FromBody] ApplyBottomLineTotalRequest request)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }

        //------------------------------------------------------------------------------------        
        // POST api/v1/order/startpoworksheetsession
        [HttpPost("startpoworksheetsession")]
        public async Task<IActionResult> StartPoWorksheetSession([FromBody] CreatePoWorksheetSessionRequest request)
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
                else if (string.IsNullOrEmpty(request.VendorId))
                {
                    response.success = false;
                    response.msg = "Vendor is required.";
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
                    response = await OrderFunc.StartPoWorksheetSession(AppConfig, UserSession, request);
                }

                return new OkObjectResult(response);
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
        // POST api/v1/order/completepoworksheetsession
        [HttpPost("completepoworksheetsession")]
        public async Task<IActionResult> CompletePoWorksheetSession([FromBody] CompletePoWorksheetSessionRequest request)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // GET api/v1/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public async Task<ActionResult<OrderLogic>> PostAsync([FromBody]OrderLogic l)
        {
            return await DoPostAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        //// DELETE api/v1/order/A0000001
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(OrderLogic));
        //}
        ////------------------------------------------------------------------------------------
    }
}