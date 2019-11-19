using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.PoSettings.PoImportance
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gLt2YTtB2afl")]
    public class PoImportanceController : AppDataController
    {
        public PoImportanceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoImportanceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"KOVgdvx5a3Ya", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"l3c00lsxhSyh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance
        [HttpGet]
        [FwControllerMethod(Id:"m3yvAg6R0B8Q", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PoImportanceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoImportanceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poimportance/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"lMZFvAGmlODB", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PoImportanceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoImportanceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poimportance
        [HttpPost]
        [FwControllerMethod(Id:"JnMyfRIJCxUF", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PoImportanceLogic>> NewAsync([FromBody]PoImportanceLogic l)
        {
            return await DoNewAsync<PoImportanceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/poimportanc/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "1K4SkaDD2v6Tj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PoImportanceLogic>> EditAsync([FromRoute] string id, [FromBody]PoImportanceLogic l)
        {
            return await DoEditAsync<PoImportanceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poimportance/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hRhHLAqtxFKq", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoImportanceLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
