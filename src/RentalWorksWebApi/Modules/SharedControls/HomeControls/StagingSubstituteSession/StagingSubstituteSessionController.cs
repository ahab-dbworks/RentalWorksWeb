//using FwStandard.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using WebApi.Controllers;
//using System.Threading.Tasks;
//using FwStandard.SqlServer;
//using System.Collections.Generic;
//using FwStandard.AppManager;
//namespace WebApi.Modules.HomeControls.StagingSubstituteSession
//{
//    [Route("api/v1/[controller]")]
//    [ApiExplorerSettings(GroupName = "agentx-v1")]
//    [FwController(Id: "QHBlFNZ4Equ0B")]
//    public class StagingSubstituteSessionController : AppDataController
//    {
//        //------------------------------------------------------------------------------------ 
//        public StagingSubstituteSessionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(StagingSubstituteSessionLogic); }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/stagingsubstitutesession/browse 
//        [HttpPost("browse")]
//        [FwControllerMethod(Id: "qHPtTq3X6AAvj", ActionType: FwControllerActionTypes.Browse)]
//        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
//        {
//            return await DoBrowseAsync(browseRequest);
//        }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/stagingsubstitutesession/exportexcelxlsx/filedownloadname 
//        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
//        [FwControllerMethod(Id: "QhwGR92yevWUI", ActionType: FwControllerActionTypes.Browse)]
//        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
//        {
//            return await DoExportExcelXlsxFileAsync(browseRequest);
//        }
//        //------------------------------------------------------------------------------------ 
//        // GET api/v1/stagingsubstitutesession 
//        [HttpGet]
//        [FwControllerMethod(Id: "QHWyhYgVmtN33", ActionType: FwControllerActionTypes.Browse)]
//        public async Task<ActionResult<IEnumerable<StagingSubstituteSessionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
//        {
//            return await DoGetAsync<StagingSubstituteSessionLogic>(pageno, pagesize, sort);
//        }
//        //------------------------------------------------------------------------------------ 
//        // GET api/v1/stagingsubstitutesession/A0000001 
//        [HttpGet("{id}")]
//        [FwControllerMethod(Id: "QitUAzmyoMKR0", ActionType: FwControllerActionTypes.View)]
//        public async Task<ActionResult<StagingSubstituteSessionLogic>> GetOneAsync([FromRoute]string id)
//        {
//            return await DoGetAsync<StagingSubstituteSessionLogic>(id);
//        }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/stagingsubstitutesession 
//        [HttpPost]
//        [FwControllerMethod(Id: "QjArJxJKLT570", ActionType: FwControllerActionTypes.New)]
//        public async Task<ActionResult<StagingSubstituteSessionLogic>> NewAsync([FromBody]StagingSubstituteSessionLogic l)
//        {
//            return await DoNewAsync<StagingSubstituteSessionLogic>(l);
//        }
//        //------------------------------------------------------------------------------------ 
//        // PUT api/v1/stagingsubstitutesession/A0000001 
//        [HttpPut("{id}")]
//        [FwControllerMethod(Id: "QjmsGllCzTvO8", ActionType: FwControllerActionTypes.Edit)]
//        public async Task<ActionResult<StagingSubstituteSessionLogic>> EditAsync([FromRoute] string id, [FromBody]StagingSubstituteSessionLogic l)
//        {
//            return await DoEditAsync<StagingSubstituteSessionLogic>(l);
//        }
//        //------------------------------------------------------------------------------------ 
//        // DELETE api/v1/stagingsubstitutesession/A0000001 
//        [HttpDelete("{id}")]
//        [FwControllerMethod(Id: "qK8Vp8HEKX9eU", ActionType: FwControllerActionTypes.Delete)]
//        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
//        {
//            return await DoDeleteAsync<StagingSubstituteSessionLogic>(id);
//        }
//        //------------------------------------------------------------------------------------ 
//    }
//}
