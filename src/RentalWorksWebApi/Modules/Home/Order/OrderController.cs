using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebLibrary;
using WebApi.Modules.Home.Quote;

namespace WebApi.Modules.Home.Order
{
    [Route("api/v1/[controller]")]
    public class OrderController : AppDataController
    {
        public OrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderLogic));
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
        // POST api/v1/order/applybottomlinedaysperweek
        [HttpPost("applybottomlinedaysperweek")]
        public async Task<IActionResult> ApplyBottomLineDaysPerWeek([FromBody] BottomLineDaysPerWeekRequest request)
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
                    bool applied = await l.ApplyBottomLineDaysPerWeek(request.RecType, request.DaysPerWeek);
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
        public async Task<IActionResult> ApplyBottomLineDiscountPercent([FromBody] BottomLineDiscountPercentRequest request)
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
                    bool applied = await l.ApplyBottomLineDiscountPercent(request.RecType, request.DiscountPercent);
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
        public async Task<IActionResult> ApplyBottomLineTotal([FromBody] BottomLineTotalRequest request)
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
                    bool applied = await l.ApplyBottomLineTotal(request.RecType, request.Total, request.IncludeTaxInTotal.Value);
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
        // GET api/v1/order
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLogic>(pageno, pagesize, sort, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/order/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLogic>(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrderLogic l)
        {
            return await DoPostAsync<OrderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/order/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/order/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}