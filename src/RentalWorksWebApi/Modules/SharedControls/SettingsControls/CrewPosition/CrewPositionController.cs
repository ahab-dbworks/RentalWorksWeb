using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.LaborSettings.LaborType;
using WebApi.Modules.Settings.LaborSettings.Position;

namespace WebApi.Modules.Settings.CrewPosition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"shA9rX1DYWp3")]
    public class CrewPositionController : AppDataController
    {
        public CrewPositionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewPositionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iJyWw0oqaK3p", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PjIMlZfHgmbJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition 
        [HttpGet]
        [FwControllerMethod(Id:"lggbX8OCswR4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CrewPositionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewPositionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewposition/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5NOtnCqsFf9G", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CrewPositionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewPositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition 
        [HttpPost]
        [FwControllerMethod(Id:"1VBc69ytRyUB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CrewPositionLogic>> NewAsync([FromBody]CrewPositionLogic l)
        {
            return await DoNewAsync<CrewPositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/crewposition/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "nae98ioV1nqff", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CrewPositionLogic>> EditAsync([FromRoute] string id, [FromBody]CrewPositionLogic l)
        {
            return await DoEditAsync<CrewPositionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crewposition/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4Mebi3QjMhfu", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CrewPositionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/validatelabortype/browse
        [HttpPost("validatelabortype/browse")]
        [FwControllerMethod(Id: "rElqQatRyId7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewposition/validaterate/browse
        [HttpPost("validaterate/browse")]
        [FwControllerMethod(Id: "fCbokeeLoNW4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PositionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
