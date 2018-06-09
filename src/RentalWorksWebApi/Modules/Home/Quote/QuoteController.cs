using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using WebLibrary;
using WebApi.Modules.Home.Order;

namespace WebApi.Modules.Home.Quote
{

    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class QuoteController : AppDataController
    {
        public QuoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/copy/A0000001
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
                QuoteLogic l = new QuoteLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<QuoteLogic>(ids))
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
        // POST api/v1/quote/createorder/A0000001
        [HttpPost("createorder/{id}")]
        public async Task<IActionResult> CreateOrder([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                QuoteLogic quote = new QuoteLogic();
                quote.SetDependencies(AppConfig, UserSession);
                if (await quote.LoadAsync<QuoteLogic>(ids))
                {
                    OrderLogic order = (OrderLogic)await quote.QuoteToOrderASync<OrderBaseLogic>();
                    return new OkObjectResult(order);
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
        // POST api/v1/quote/cancel/A0000001
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelQuote([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                QuoteLogic quote = new QuoteLogic();
                quote.SetDependencies(AppConfig, UserSession);
                if (await quote.LoadAsync<QuoteLogic>(ids))
                {
                    await quote.CancelQuoteASync();
                    return new OkObjectResult(quote);
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
        // POST api/v1/quote/uncancel/A0000001
        [HttpPost("uncancel/{id}")]
        public async Task<IActionResult> UncancelQuote([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                QuoteLogic quote = new QuoteLogic();
                quote.SetDependencies(AppConfig, UserSession);
                if (await quote.LoadAsync<QuoteLogic>(ids))
                {
                    await quote.UncancelQuoteASync();
                    return new OkObjectResult(quote);
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

                QuoteLogic l = new QuoteLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<QuoteLogic>(ids))
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

                QuoteLogic l = new QuoteLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<QuoteLogic>(ids))
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

                QuoteLogic l = new QuoteLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<QuoteLogic>(ids))
                {
                    bool applied = await l.ApplyBottomLineTotal(request.RecType, request.TotalType, request.Total, request.IncludeTaxInTotal.Value);
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
        // GET api/v1/quote
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuoteLogic>(pageno, pagesize, sort, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuoteLogic>(id, typeof(QuoteLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]QuoteLogic l)
        {
            return await DoPostAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        //// DELETE api/v1/quote/A0000001
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(QuoteLogic));
        //}
        ////------------------------------------------------------------------------------------
        // POST api/v1/quote/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}