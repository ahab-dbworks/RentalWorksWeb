using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;
using WebApi.Modules.Settings.WidgetDateBehavior;
using WebApi.Modules.Settings.NumberFormat;

namespace WebApi.Modules.Settings.WidgetSettings.Widget
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "QLfJikDW0foC1")]
    public class WidgetController : AppDataController
    {
        public WidgetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WidgetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "BWmK2P2RqJGda", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "7HgALUtIrQfvS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget 
        [HttpGet]
        [FwControllerMethod(Id: "jp2h2fRhrJEQt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WidgetLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WidgetLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "FxGaJ0Uu065nz", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WidgetLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WidgetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widget 
        [HttpPost]
        [FwControllerMethod(Id: "ad31XNuvp128V", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WidgetLogic>> NewAsync([FromBody]WidgetLogic l)
        {
            return await DoNewAsync<WidgetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/widget/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "AIYVp4zW05uNJ", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WidgetLogic>> EditAsync([FromRoute] string id, [FromBody]WidgetLogic l)
        {
            return await DoEditAsync<WidgetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/widget/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "Wv3dxWeI0piCr", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WidgetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widget/loadbyname/ordersbystatus
        [HttpGet("loadbyname/{widgetApiName}")]
        [FwControllerMethod(Id: "CSCjPzhW5pIbB", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<Widget>> LoadByName([FromRoute]string widgetApiName, int dataPoints, string locationId, string warehouseId, string departmentId, string dateBehaviorId, string dateField, DateTime? fromDate, DateTime? toDate, bool? stacked)
        {
            return await DoGetWidget(widgetApiName, dataPoints: dataPoints, locationId: locationId, dateBehaviorId: dateBehaviorId, dateField: dateField, fromDate: fromDate, toDate: toDate, stacked: stacked);
        }
        //------------------------------------------------------------------------------------
        private async Task<ActionResult<Widget>> DoGetWidget(string widgetName, int dataPoints = 0, string locationId = "", string warehouseId = "", string departmentId = "", string dateBehaviorId = "", string dateField = "", DateTime? fromDate = null, DateTime? toDate = null, bool? stacked = false)
        {
            try
            {
                string widgetId = await AppFunc.GetStringDataAsync(AppConfig, "widget", "apiname", widgetName, "widgetid");

                WidgetLogic l = new WidgetLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.WidgetId = widgetId;
                await l.LoadAsync<WidgetLogic>();
                Widget w = new Widget(l);
                w.dateBehaviorId = dateBehaviorId;
                w.dateField = dateField;
                w.fromDate = fromDate;
                w.toDate = toDate;
                w.SetDbConfig(this.AppConfig.DatabaseSettings);
                w.dataPoints = dataPoints;
                w.locationId = locationId;
                w.warehouseId = warehouseId;
                w.departmentId = departmentId;
                w.stacked = stacked.GetValueOrDefault(false);
                bool b = w.LoadAsync().Result;
                return new OkObjectResult(w);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/widget/validatedatebehavior/browse
        [HttpPost("validatedatebehavior/browse")]
        [FwControllerMethod(Id: "ARYDB6o1rEBt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDateBehaviorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WidgetDateBehaviorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/widget/validateaxisnumberformat/browse
        [HttpPost("validateaxisnumberformat/browse")]
        [FwControllerMethod(Id: "2szc7FWOcaRq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAxisNumberFormatBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<NumberFormatLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/widget/validatedatanumberformat/browse
        [HttpPost("validatedatanumberformat/browse")]
        [FwControllerMethod(Id: "MrSmLpFQQRKA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDataNumberFormatBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<NumberFormatLogic>(browseRequest);
        }
    }
}
