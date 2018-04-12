using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.Settings.Widget
{
    [Route("api/v1/[controller]")]
    public class WidgetController : AppDataController
    {
        public WidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WidgetLogic>(pageno, pagesize, sort, typeof(WidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WidgetLogic>(id, typeof(WidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WidgetLogic l)
        {
            return await DoPostAsync<WidgetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/widget/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget/loadbyname/ordersbystatus
        [HttpGet("loadbyname/{widgetApiName}")]
        public async Task<IActionResult> LoadByName([FromRoute]string widgetApiName)
        {
            return await DoGetWidget(widgetApiName);
        }
        //------------------------------------------------------------------------------------
        [HttpGet("loadbyname/{widgetApiName}/{dataPoints}")]
        public async Task<IActionResult> LoadByName([FromRoute]string widgetApiName, [FromRoute]int dataPoints)
        {
            return await DoGetWidget(widgetApiName, dataPoints);
        }
        //------------------------------------------------------------------------------------
        private async Task<IActionResult> DoGetWidget(string widgetName, int dataPoints=0)
        {
            try
            {
                Widget w = null;
                switch (widgetName)
                {
                    case "quotesbystatus":
                        w = new WidgetQuotesByStatus();
                        break;
                    case "ordersbystatus":
                        w = new WidgetOrdersByStatus();
                        break;
                    case "ordersbyagent":
                        w = new WidgetOrdersByAgent();
                        break;
                    case "dealsbytype":
                        w = new WidgetDealsByType();
                        break;
                    case "customersbytype":
                        w = new WidgetCustomersByType();
                        break;
                    case "repairsbywarehouse":
                        w = new WidgetRepairsByWarehouse();
                        break;
                    case "billingbyagentbymonth":
                        w = new WidgetBillingByAgentByMonth();
                        break;
                    default:
                        w = null;
                        break;
                }
                if (w == null)
                {
                    FwApiException widgetException = new FwApiException();
                    widgetException.StatusCode = StatusCodes.Status500InternalServerError;
                    widgetException.Message = "Invalid widget name: " + widgetName;
                    return StatusCode(widgetException.StatusCode, widgetException);
                }
                else
                {
                    w.SetDbConfig(this.AppConfig.DatabaseSettings);
                    w.dataPoints = dataPoints;
                    bool b = w.LoadAsync().Result;
                    return new OkObjectResult(w);
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