using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.SystemUpdateHistoryLog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "pqA0vJ5YCXp3X")]
    public class SystemUpdateHistoryLogController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public SystemUpdateHistoryLogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SystemUpdateHistoryLogLogic); }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdatehistorylog/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "PqhP2MtMYep1b", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/systemupdatehistorylog/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "prb1sr7M3nLId", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemupdatehistorylog 
        [HttpGet]
        [FwControllerMethod(Id: "pRkxxy7xMM0r5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SystemUpdateHistoryLogLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SystemUpdateHistoryLogLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/systemupdatehistorylog/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "prn2OnxJKp1vD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SystemUpdateHistoryLogLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SystemUpdateHistoryLogLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //// POST api/v1/systemupdatehistorylog 
        //[HttpPost]
        //[FwControllerMethod(Id: "prrb34caqA9Yg", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<SystemUpdateHistoryLogLogic>> NewAsync([FromBody]SystemUpdateHistoryLogLogic l)
        //{
        //    return await DoNewAsync<SystemUpdateHistoryLogLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// PUT api/v1/systemupdatehistorylog/A0000001 
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "pRtHCtpqbPY51", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<SystemUpdateHistoryLogLogic>> EditAsync([FromRoute] string id, [FromBody]SystemUpdateHistoryLogLogic l)
        //{
        //    return await DoEditAsync<SystemUpdateHistoryLogLogic>(l);
        //}
        ////------------------------------------------------------------------------------------ 
        //// DELETE api/v1/systemupdatehistorylog/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "pRzfaijMm5E3x", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<SystemUpdateHistoryLogLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
