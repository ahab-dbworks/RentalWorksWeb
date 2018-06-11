using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Widget
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WidgetController : AppDataController
    {
        public WidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WidgetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WidgetLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
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
        // GET api/v1/widget/loadbyname/ordersbystatus
        [HttpGet("loadbyname/{widgetApiName}")]
        public async Task<IActionResult> LoadByName([FromRoute]string widgetApiName, int dataPoints, string locationId, string warehouseId, string departmentId)
        {
            return await DoGetWidget(widgetApiName, dataPoints: dataPoints, locationId: locationId);
        }
        //------------------------------------------------------------------------------------
        private async Task<IActionResult> DoGetWidget(string widgetName, int dataPoints = 0, string locationId = "", string warehouseId = "", string departmentId = "")
        {
            try
            {
                string widgetId = await AppFunc.GetStringDataAsync(AppConfig, "widget", "apiname", widgetName, "widgetid");

                WidgetLogic l = new WidgetLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.WidgetId = widgetId;
                await l.LoadAsync<WidgetLogic>();
                Widget w = new Widget();
                if (widgetName.Equals("billingbyagentbymonth"))
                {
                    w = new WidgetBillingByAgentByMonth();
                }
                else
                {
                    w.options.title.text = l.Widget;
                    w.options.title.fontSize = 20;
                    w.type = l.DefaultType;
                    w.sql = l.Sql;
                    w.counterFieldName = l.CounterFieldName;
                    w.labelFieldName = l.LabelFieldName;
                }
                w.SetDbConfig(this.AppConfig.DatabaseSettings);
                w.dataPoints = dataPoints;
                w.locationId = locationId;
                bool b = w.LoadAsync().Result;
                return new OkObjectResult(w);
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