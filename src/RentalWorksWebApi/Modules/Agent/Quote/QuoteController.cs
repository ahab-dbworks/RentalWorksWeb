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
using WebApi.Logic;
using WebApi.Modules.Agent.Order;
using WebLibrary;
using static WebApi.Modules.Home.DealOrder.DealOrderRecord;

namespace WebApi.Modules.Agent.Quote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "jFkSBEur1dluU")]
    public class QuoteController : AppDataController
    {
        public QuoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(QuoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "5aghkpZ8BLC68")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            if (!await AppFunc.ValidateBrowseRequestActiveViewDealId(this.AppConfig, this.UserSession, browseRequest))
            {
                return new BadRequestResult();
            }
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quote/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "bV65XBHFpqRzf")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Locked", RwGlobals.QUOTE_ORDER_LOCKED_COLOR);
            legend.Add("On Hold", RwGlobals.QUOTE_ORDER_ON_HOLD_COLOR);
            legend.Add("Reserved", RwGlobals.QUOTE_RESERVED_COLOR);
            legend.Add("No Charge", RwGlobals.QUOTE_ORDER_NO_CHARGE_COLOR);
            legend.Add("Foreign Currency", RwGlobals.FOREIGN_CURRENCY_COLOR);
            legend.Add("Multi-Warehouse", RwGlobals.QUOTE_ORDER_MULTI_WAREHOUSE_COLOR);
            legend.Add("Quote Request", RwGlobals.QUOTE_REQUEST_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "5aghkpZ8BLC68")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/copytoquote/A0000001
        [HttpPost("copytoquote/{id}")]
        [FwControllerMethod(Id: "8eK9AJhpOq8c4")]
        public async Task<ActionResult<QuoteLogic>> CopyToQuote([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/quote/copytoorder/A0000001
        [HttpPost("copytoorder/{id}")]
        [FwControllerMethod(Id: "8eK9AJhpOq8c4")]
        public async Task<ActionResult<OrderLogic>> CopyToOrder([FromRoute]string id, [FromBody] QuoteOrderCopyRequest copyRequest)
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
        [FwControllerMethod(Id: "jzLmFvzdy5hE1")]
        public async Task<ActionResult<OrderLogic>> CreateOrder([FromRoute]string id)
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
        // POST api/v1/quote/reserve/A0000001
        [HttpPost("reserve/{id}")]
        [FwControllerMethod(Id: "1oBE7m2rBjxhm")]
        public async Task<ActionResult<QuoteLogic>> Reserve([FromRoute]string id)
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
                    ReserveQuoteResponse response = await l.Reserve();
                    if (response.success)
                    {
                        await l.LoadAsync<QuoteLogic>(ids);
                        response.quote = l;
                        return new OkObjectResult(response);
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
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/quote/createnewversion/A0000001
        [HttpPost("createnewversion/{id}")]
        [FwControllerMethod(Id: "6KMadUFDT4cX4")]
        public async Task<ActionResult<QuoteLogic>> CreateNewVersion([FromRoute]string id)
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
                    QuoteLogic newVersion = await quote.CreateNewVersionASync();
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
        // POST api/v1/quote/makequoteactive/A0000001
        [HttpPost("makequoteactive/{id}")]
        [FwControllerMethod(Id: "7mrZ4Q8ShsJ")]
        public async Task<ActionResult<TSpStatusResponse>> MakeQuoteActive([FromRoute]string id)
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
                    TSpStatusResponse response = await quote.MakeQuoteActiveAsync();
                    if (response.success)
                    {
                        return new OkObjectResult(response);
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
        [FwControllerMethod(Id: "dpH0uCuEp3E89")]
        public async Task<ActionResult<QuoteLogic>> CancelQuote([FromRoute]string id)
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
        [FwControllerMethod(Id: "i3Lb6rWQdXHSm")]
        public async Task<ActionResult<QuoteLogic>> UncancelQuote([FromRoute]string id)
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
        [FwControllerMethod(Id: "B5zs0jNJlYt9k")]
        public async Task<ActionResult<bool>> ApplyBottomLineDaysPerWeek([FromBody] ApplyBottomLineDaysPerWeekRequest request)
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
        [FwControllerMethod(Id: "U6LMoC2hxtR8w")]
        public async Task<ActionResult<bool>> ApplyBottomLineDiscountPercent([FromBody] ApplyBottomLineDiscountPercentRequest request)
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
        [FwControllerMethod(Id: "IyKyL5lOiP6pU")]
        public async Task<ActionResult<bool>> ApplyBottomLineTotal([FromBody] ApplyBottomLineTotalRequest request)
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

        // POST api/v1/quote/changeofficelocation/A0000001
        [HttpPost("changeofficelocation/{id}")]
        [FwControllerMethod(Id: "eu2FcQiK9adgk")]
        public async Task<ActionResult<ChangeOrderOfficeLocationResponse>> ChangeOfficeLocation([FromRoute]string id, [FromBody] ChangeOrderOfficeLocationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                QuoteLogic Quote = new QuoteLogic();
                Quote.SetDependencies(AppConfig, UserSession);
                if (await Quote.LoadAsync<QuoteLogic>(ids))
                {
                    ChangeOrderOfficeLocationResponse response = await Quote.ChangeOfficeLocationASync(request);
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

        // GET api/v1/quote
        [HttpGet]
        [FwControllerMethod(Id: "pGYcsCb0FxCEC")]
        public async Task<ActionResult<IEnumerable<QuoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "R5IFp8DPp3PHh")]
        public async Task<ActionResult<QuoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote
        [HttpPost]
        [FwControllerMethod(Id: "kw5bRMvEGkPWY")]
        public async Task<ActionResult<QuoteLogic>> PostAsync([FromBody]QuoteLogic l)
        {
            return await DoPostAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/quote/submit/A0000001
        [HttpPost("submit/{id}")]
        [FwControllerMethod(Id: "85YP7Omvhhmh")]
        public async Task<ActionResult<QuoteLogic>> SubmitQuote([FromRoute]string id)
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
                    await quote.SubmitQuoteASync();
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
        // POST api/v1/quote/activatequoterequest/A0000001
        [HttpPost("activatequoterequest/{id}")]
        [FwControllerMethod(Id: "IAxyDXaKQVQt")]
        public async Task<ActionResult<QuoteLogic>> ActivateQuoteRequest([FromRoute]string id)
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
                    QuoteLogic newVersion = await quote.ActivateQuoteRequestASync();
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
    }
}
