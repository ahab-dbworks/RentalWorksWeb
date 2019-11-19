using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.Settings.WidgetGroup
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "BXv7mQIbXokIW")]
    public class WidgetGroupController : AppDataController
    {
        public WidgetGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WidgetGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetgroup/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "SLvW92olWurb", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetgroup/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "bVVxsEfnKgM4M", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widgetgroup 
        [HttpGet]
        [FwControllerMethod(Id: "Qnb8a5fsSrgPU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WidgetGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WidgetGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/widgetgroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "ExEZMslmfoXla", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<WidgetGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WidgetGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/widgetgroup 
        [HttpPost]
        [FwControllerMethod(Id: "hYDiwv50XuxCQ", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WidgetGroupLogic>> NewAsync([FromBody]WidgetGroupLogic l)
        {
            return await DoNewAsync<WidgetGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/widgetgroup/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "LIi7TJSAChfNM", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WidgetGroupLogic>> EditAsync([FromRoute] string id, [FromBody]WidgetGroupLogic l)
        {
            return await DoEditAsync<WidgetGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/widgetgroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "IhAcr3nEyENog", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WidgetGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
