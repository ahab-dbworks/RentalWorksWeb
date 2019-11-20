using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WorkWeekSettings.WorkWeek
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"hRNv34ONOUmB7")]
    public class WorkWeekController : AppDataController
    {
        public WorkWeekController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WorkWeekLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"H3NcZgXv6uyN7", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"DL5kC0LrWmHMQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/workweek 
        [HttpGet]
        [FwControllerMethod(Id:"3qyQrJByAeJQ5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WorkWeekLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WorkWeekLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/workweek/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3jJCa9tGzjhLA", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WorkWeekLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WorkWeekLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/workweek 
        [HttpPost]
        [FwControllerMethod(Id:"IHSnmLTt5Q3oB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WorkWeekLogic>> NewAsync([FromBody]WorkWeekLogic l)
        {
            return await DoNewAsync<WorkWeekLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/workweek/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "bYsTbEU01Meky", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WorkWeekLogic>> EditAsync([FromRoute] string id, [FromBody]WorkWeekLogic l)
        {
            return await DoEditAsync<WorkWeekLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/workweek/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"LiRanbX72kNWt", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WorkWeekLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
