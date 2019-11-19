using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.PoSettings.PoApproverRole
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HdPKvHGhi3zf")]
    public class PoApproverRoleController : AppDataController
    {
        public PoApproverRoleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoApproverRoleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"JW91c1zDCAB5", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SVtpscDCdN2m", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole
        [HttpGet]
        [FwControllerMethod(Id:"UPeBwxFNEwAH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PoApproverRoleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoApproverRoleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poapproverrole/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"m9AvhLfMz08t", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PoApproverRoleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoApproverRoleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poapproverrole
        [HttpPost]
        [FwControllerMethod(Id:"YnRXfWaGFC83", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PoApproverRoleLogic>> NewAsync([FromBody]PoApproverRoleLogic l)
        {
            return await DoNewAsync<PoApproverRoleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/poapproverrol/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xw0hV5dI8pl2B", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PoApproverRoleLogic>> EditAsync([FromRoute] string id, [FromBody]PoApproverRoleLogic l)
        {
            return await DoEditAsync<PoApproverRoleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poapproverrole/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"UUeAxsOf5Y4K", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoApproverRoleLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
