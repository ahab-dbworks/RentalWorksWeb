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
using WebApi;
using static WebApi.Modules.HomeControls.DealOrder.DealOrderRecord;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Settings.OrderSettings.OrderType;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Contact;
using WebApi.Modules.Settings.OrderSettings.MarketType;
using WebApi.Modules.Settings.OrderSettings.MarketSegment;
using WebApi.Modules.Settings.MarketSegmentJob;
using WebApi.Modules.Settings.DocumentSettings.CoverLetter;
using WebApi.Modules.Settings.DocumentSettings.TermsConditions;
using WebApi.Modules.Settings.BillingCycleSettings.BillingCycle;
using WebApi.Modules.Settings.PaymentSettings.PaymentTerms;
using WebApi.Modules.Settings.PaymentSettings.PaymentType;
using WebApi.Modules.Settings.CurrencySettings.Currency;
using WebApi.Modules.Settings.TaxSettings.TaxOption;
using WebApi.Modules.Settings.OrderSettings.DiscountReason;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.ShipViaSettings.ShipVia;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;

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
        [FwControllerMethod(Id: "ITsUfVj2zTAH", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "bV65XBHFpqRzf", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "5aghkpZ8BLC68", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/copytoquote/A0000001
        [HttpPost("copytoquote/{id}")]
        [FwControllerMethod(Id: "xH1RcWMEbgxE", ActionType: FwControllerActionTypes.Option, Caption: "Copy to Quote")]
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
        [FwControllerMethod(Id: "8eK9AJhpOq8c4", ActionType: FwControllerActionTypes.Option, Caption: "Copy to Order")]
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
        [FwControllerMethod(Id: "jzLmFvzdy5hE1", ActionType: FwControllerActionTypes.Option, Caption: "Create Order")]
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
        [FwControllerMethod(Id: "1oBE7m2rBjxhm", ActionType: FwControllerActionTypes.Option, Caption: "Reserve")]
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
        [FwControllerMethod(Id: "6KMadUFDT4cX4", ActionType: FwControllerActionTypes.Option, Caption: "Create New Version")]
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
        [FwControllerMethod(Id: "7mrZ4Q8ShsJ", ActionType: FwControllerActionTypes.Option, Caption: "Make Quote Active")]
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
        [FwControllerMethod(Id: "dpH0uCuEp3E89", ActionType: FwControllerActionTypes.Option, Caption: "Cancel Quote")]
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
        [FwControllerMethod(Id: "i3Lb6rWQdXHSm", ActionType: FwControllerActionTypes.Option, Caption: "Uncancel Quote")]
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
        // POST api/v1/quote/applybottomlinedaysperweek
        [HttpPost("applybottomlinedaysperweek")]
        [FwControllerMethod(Id: "B5zs0jNJlYt9k", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Days Per Week")]
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
        // POST api/v1/quote/applybottomlinediscountpercent
        [HttpPost("applybottomlinediscountpercent")]
        [FwControllerMethod(Id: "U6LMoC2hxtR8w", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Discount Percent")]
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
        // POST api/v1/quote/applybottomlinetotal
        [HttpPost("applybottomlinetotal")]
        [FwControllerMethod(Id: "IyKyL5lOiP6pU", ActionType: FwControllerActionTypes.Edit, Caption: "Apply Bottom Line Total")]
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
        [FwControllerMethod(Id: "eu2FcQiK9adgk", ActionType: FwControllerActionTypes.Browse)]
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
        [FwControllerMethod(Id: "pGYcsCb0FxCEC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<QuoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<QuoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/quote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "R5IFp8DPp3PHh", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<QuoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<QuoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote
        [HttpPost]
        [FwControllerMethod(Id: "kw5bRMvEGkPWY", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<QuoteLogic>> NewAsync([FromBody]QuoteLogic l)
        {
            return await DoNewAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------       
        // PUT api/v1/quot/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "JS8b45qWSjLpI", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<QuoteLogic>> EditAsync([FromRoute] string id, [FromBody]QuoteLogic l)
        {
            return await DoEditAsync<QuoteLogic>(l);
        }
        //------------------------------------------------------------------------------------       
        // POST api/v1/quote/submit/A0000001
        [HttpPost("submit/{id}")]
        [FwControllerMethod(Id: "85YP7Omvhhmh", ActionType: FwControllerActionTypes.Option, Caption: "Submit Quote")]
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
        [FwControllerMethod(Id: "IAxyDXaKQVQt", ActionType: FwControllerActionTypes.Option, Caption: "Activate Quote Request")]
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
        // POST api/v1/quote/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "adGmzDA6sgE1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatedeal/browse 
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "TEa2rI4tVvDt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateratetype/browse 
        [HttpPost("validateratetype/browse")]
        [FwControllerMethod(Id: "uLjB7Qt1OxIo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateordertype/browse 
        [HttpPost("validateordertype/browse")]
        [FwControllerMethod(Id: "pKIXsHcKheu3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOrderTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateagent/browse 
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "ugj6CmIPKo2X", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateprojectmanager/browse 
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "u89KWMmx6VNQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateoutsidesalesrepresentative/browse 
        [HttpPost("validateoutsidesalesrepresentative/browse")]
        [FwControllerMethod(Id: "agbos3kqrpg0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutsideSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatemarkettype/browse 
        [HttpPost("validatemarkettype/browse")]
        [FwControllerMethod(Id: "IxzCX3NbRUDt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatemarketsegment/browse 
        [HttpPost("validatemarketsegment/browse")]
        [FwControllerMethod(Id: "IBWF277IDC6w", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketSegmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketSegmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatemarketsegmentjob/browse 
        [HttpPost("validatemarketsegmentjob/browse")]
        [FwControllerMethod(Id: "kvxQcMHe1lcA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMarketSegmentJobBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MarketSegmentJobLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatecoverletter/browse 
        [HttpPost("validatecoverletter/browse")]
        [FwControllerMethod(Id: "oSIdhL8qMRO1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCoverLetterBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CoverLetterLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatetermsconditions/browse 
        [HttpPost("validatetermsconditions/browse")]
        [FwControllerMethod(Id: "gPyusIwgBcsD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTermsConditionsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TermsConditionsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatebillingcycle/browse 
        [HttpPost("validatebillingcycle/browse")]
        [FwControllerMethod(Id: "F78fnzBzzxsj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillingCycleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BillingCycleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatepaymentterms/browse 
        [HttpPost("validatepaymentterms/browse")]
        [FwControllerMethod(Id: "y4A1DQTCPGKx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTermsBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTermsLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatepaymenttype/browse 
        [HttpPost("validatepaymenttype/browse")]
        [FwControllerMethod(Id: "ooWtW68kBeHk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePaymentTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PaymentTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatecurrency/browse 
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "N2corqBSyp5x", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatetaxoption/browse 
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "jEMUt0X9xL19", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOptionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatediscountreason/browse 
        [HttpPost("validatediscountreason/browse")]
        [FwControllerMethod(Id: "LjLDygQtwpoK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDiscountReasonBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DiscountReasonLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateissuedtocountry/browse 
        [HttpPost("validateissuedtocountry/browse")]
        [FwControllerMethod(Id: "NFkze199szls", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIssuedToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateoutdeliverycarrier/browse 
        [HttpPost("validateoutdeliverycarrier/browse")]
        [FwControllerMethod(Id: "to2LrIXcacLj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateoutdeliveryshipvia/browse 
        [HttpPost("validateoutdeliveryshipvia/browse")]
        [FwControllerMethod(Id: "O8vrHLb3rcgW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateindeliverycarrier/browse 
        [HttpPost("validateindeliverycarrier/browse")]
        [FwControllerMethod(Id: "Oz4GM2Rn8tll", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateindeliveryshipvia/browse 
        [HttpPost("validateindeliveryshipvia/browse")]
        [FwControllerMethod(Id: "pfcXBmJL4cUf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateoutdeliverytocountry/browse 
        [HttpPost("validateoutdeliverytocountry/browse")]
        [FwControllerMethod(Id: "FRBPyqbrhf72", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateindeliverytocountry/browse 
        [HttpPost("validateindeliverytocountry/browse")]
        [FwControllerMethod(Id: "OszhvzlvYkcC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "JnCeCF3LNXa4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatewarehouse/browse 
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "BbXRs3cjjlll", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/quote/validatebilltocountry/browse 
        [HttpPost("validatebilltocountry/browse")]
        [FwControllerMethod(Id: "jXMkDxX7rMHU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateBillToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
    }
}
