using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.SystemUpdateHistory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "M9KMnPVOQgT43")]
    public class SystemUpdateHistoryController : AppDataController
    {
        public SystemUpdateHistoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SystemUpdateHistoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdatehistory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "m9p4Bzkl3cQRv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdatehistory/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "mA88cJJO1h9D2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemupdatehistory 
        [HttpGet]
        [FwControllerMethod(Id: "Ma8pxPLORHOZr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SystemUpdateHistoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SystemUpdateHistoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemupdatehistory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "mAqsHxmLePGQi", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SystemUpdateHistoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SystemUpdateHistoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdatehistory 
        [HttpPost]
        [FwControllerMethod(Id: "mBaOQTQwGLCpN", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SystemUpdateHistoryLogic>> NewAsync([FromBody]SystemUpdateHistoryLogic l)
        {
            return await DoNewAsync<SystemUpdateHistoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// PUT api/v1/systemupdatehistory/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "MbSVXHG5ZTBDp", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<SystemUpdateHistoryLogic>> EditAsync([FromRoute] string id, [FromBody]SystemUpdateHistoryLogic l)
        //{
        //    return await DoEditAsync<SystemUpdateHistoryLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/systemupdatehistory/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "MbxAYgnwDirFK", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<SystemUpdateHistoryLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
