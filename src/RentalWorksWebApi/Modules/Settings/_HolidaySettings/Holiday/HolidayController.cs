using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.HolidaySettings.Holiday
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"nZBYaMILxWSm")]
    public class HolidayController : AppDataController
    {
        public HolidayController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(HolidayLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lnXcVaqQR3oP", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"cvQShxMCmu7R", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday 
        [HttpGet]
        [FwControllerMethod(Id:"0Q9OyAPKUTUK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<HolidayLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<HolidayLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/holiday/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"lAtTULC52ePX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<HolidayLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<HolidayLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/holiday 
        [HttpPost]
        [FwControllerMethod(Id:"ZR9LWHlv33XT", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<HolidayLogic>> NewAsync([FromBody]HolidayLogic l)
        {
            return await DoNewAsync<HolidayLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/holiday/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "dtQ062hKhm0LY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<HolidayLogic>> EditAsync([FromRoute] string id, [FromBody]HolidayLogic l)
        {
            return await DoEditAsync<HolidayLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/holiday/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"ZOTYVWL3Q1Tm", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<HolidayLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
