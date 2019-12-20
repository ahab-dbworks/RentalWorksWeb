using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.SystemNumber
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "aUMum8mzxVrWc")]
    public class SystemNumberController : AppDataController
    {
        public SystemNumberController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SystemNumberLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemnumber/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "Auuck8llnimse", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemnumber/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "AuYtVHNrGRBgH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemnumber 
        [HttpGet]
        [FwControllerMethod(Id: "AvpRxKbFO8U9Z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SystemNumberLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SystemNumberLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemnumber/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "AW0zw8pmjQHlF", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SystemNumberLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SystemNumberLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/systemnumber 
        //[HttpPost]
        //[FwControllerMethod(Id: "AWH1xVSm6tPyC", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<SystemNumberLogic>> NewAsync([FromBody]SystemNumberLogic l)
        //{
        //    return await DoNewAsync<SystemNumberLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        // PUT api/v1/systemnumber/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "awrvnfPcAPbwj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SystemNumberLogic>> EditAsync([FromRoute] string id, [FromBody]SystemNumberLogic l)
        {
            return await DoEditAsync<SystemNumberLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/systemnumber/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "aWtf0Ui6kKwTx", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<SystemNumberLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
