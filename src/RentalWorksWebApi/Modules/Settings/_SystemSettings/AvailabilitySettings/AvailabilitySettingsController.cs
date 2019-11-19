using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.SystemSettings.AvailabilitySettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "UXYMLInJl6JMP")]
    public class AvailabilitySettingsController : AppDataController
    {
        public AvailabilitySettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AvailabilitySettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "mDoMpfZJiGN9N", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "NvxY1l4VAIGdt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitysettings 
        [HttpGet]
        [FwControllerMethod(Id: "vY1IOPjEs7oS8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AvailabilitySettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AvailabilitySettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/availabilitysettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "x2qH5CpVJCLJO", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AvailabilitySettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/availabilitysettings 
        [HttpPost]
        [FwControllerMethod(Id: "15NYtyWYoQTYV", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> NewAsync([FromBody]AvailabilitySettingsLogic l)
        {
            return await DoNewAsync<AvailabilitySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/availabilitysettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "uEqrPXxnOYQZa", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AvailabilitySettingsLogic>> EditAsync([FromRoute] string id, [FromBody]AvailabilitySettingsLogic l)
        {
            return await DoEditAsync<AvailabilitySettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/availabilitysettings/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "xBQUWdcisNiim", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <AvailabilitySettingsLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
