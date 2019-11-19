using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.AppManager;

namespace WebApi.Modules.Settings.InventorySettings.RetiredReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "hktLnLB9qF7k")]
    public class RetiredReasonController : AppDataController
    {
        public RetiredReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RetiredReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "WJh6ML7YxQdQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "gngt0UVFHtmI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/retiredreason
        [HttpGet]
        [FwControllerMethod(Id: "NORQE8jmqRbc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RetiredReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RetiredReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/retiredreason/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "zIfc07dBhMEa", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RetiredReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RetiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/retiredreason
        [HttpPost]
        [FwControllerMethod(Id: "FjjESCMExIs8", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<RetiredReasonLogic>> NewAsync([FromBody]RetiredReasonLogic l)
        {
            return await DoNewAsync<RetiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/retiredreaso/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "3PdsH4yxkS0H3", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RetiredReasonLogic>> EditAsync([FromRoute] string id, [FromBody]RetiredReasonLogic l)
        {
            return await DoEditAsync<RetiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/retiredreason/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "3quyI1Aymd4s", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RetiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}