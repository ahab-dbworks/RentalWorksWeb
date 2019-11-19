using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using FwStandard.Modules.Administrator.Alert;

namespace WebApi.Modules.Administrator.Alert
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "gFfpaR5mDAzX")]
    public class AlertController : AppDataController
    {
        public AlertController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AlertLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alert/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "dwocCjgaMYHU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alert/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "H6BWZRc64s6t", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alert 
        [HttpGet]
        [FwControllerMethod(Id: "M9CcdF03PawUf", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AlertLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AlertLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alert/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "fOh8YegRXqDA", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AlertLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AlertLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alert 
        [HttpPost]
        [FwControllerMethod(Id: "OoG97Z6p62HK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AlertLogic>> NewAsync([FromBody]AlertLogic l)
        {
            return await DoNewAsync<AlertLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/alert/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "OuWtm3VvxLI0b", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AlertLogic>> EditAsync([FromRoute] string id, [FromBody]AlertLogic l)
        {
            return await DoEditAsync<AlertLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/alert/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "NDPBeQE9TPDy", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AlertLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
