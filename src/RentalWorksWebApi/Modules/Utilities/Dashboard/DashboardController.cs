using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using FwStandard.AppManager;
using WebApi.Modules.Settings.WidgetSettings.Widget;
using WebApi.Logic;

namespace WebApi.Modules.Utilities.Dashboard
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "UdmOOUGqu0lKd")]
    public class DashboardController : AppDataController
    {
        public DashboardController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = null; }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/dashboard/loadwidgetbyname/xxwidgetnamexx
        [HttpGet("loadwidgetbyname/{widgetApiName}")]
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
    }
}
