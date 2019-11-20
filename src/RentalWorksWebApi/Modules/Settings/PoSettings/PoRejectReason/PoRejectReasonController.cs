using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.PoSettings.PoRejectReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"xwTGYRx4Gg21")]
    public class PoRejectReasonController : AppDataController
    {
        public PoRejectReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoRejectReasonLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"L58GHw3jnm3C", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"sMw4jWeUScim", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/porejectreason 
        [HttpGet]
        [FwControllerMethod(Id:"ygzYEaPHwRyx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PoRejectReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoRejectReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/porejectreason/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ugUzHW3PQJHv", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PoRejectReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoRejectReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/porejectreason 
        [HttpPost]
        [FwControllerMethod(Id:"w0N39wuQBavt", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PoRejectReasonLogic>> NewAsync([FromBody]PoRejectReasonLogic l)
        {
            return await DoNewAsync<PoRejectReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/porejectreason/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "V4BnrhEHzNwQL", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PoRejectReasonLogic>> EditAsync([FromRoute] string id, [FromBody]PoRejectReasonLogic l)
        {
            return await DoEditAsync<PoRejectReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/porejectreason/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"sfY6Gp6ymVZ4", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoRejectReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
