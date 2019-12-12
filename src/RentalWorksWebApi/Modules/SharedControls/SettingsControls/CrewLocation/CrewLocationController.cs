using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;

namespace WebApi.Modules.Settings.CrewLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"vCrMyhsLCP7h")]
    public class CrewLocationController : AppDataController
    {
        public CrewLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewlocation/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"UGcwmVnnTP3Q", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"6xJAKeSqoSlj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewlocation 
        [HttpGet]
        [FwControllerMethod(Id:"y9MYktG0fRgM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CrewLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/crewlocation/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"joEg5wrCaj07", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CrewLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewlocation 
        [HttpPost]
        [FwControllerMethod(Id:"drLg05zi7HQU", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CrewLocationLogic>> NewAsync([FromBody]CrewLocationLogic l)
        {
            return await DoNewAsync<CrewLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/crewlocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "7JNJs6qahHu7S", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CrewLocationLogic>> EditAsync([FromRoute] string id, [FromBody]CrewLocationLogic l)
        {
            return await DoEditAsync<CrewLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/crewlocation/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"VqleaLKvzSDt", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CrewLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/crewlocation/validatelocation/browse
        [HttpPost("validatelocation/browse")]
        [FwControllerMethod(Id: "gSO4kORNDSAe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
    }
}
