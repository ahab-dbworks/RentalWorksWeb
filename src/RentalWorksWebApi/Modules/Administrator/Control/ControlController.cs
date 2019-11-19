//using FwStandard.AppManager;
//using FwStandard.SqlServer;
//using System.Collections.Generic;
//using FwStandard.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using WebApi.Controllers;
//using System.Threading.Tasks;

//namespace WebApi.Modules.Administrator.Control
//{
//    [Route("api/v1/[controller]")]
//    [ApiExplorerSettings(GroupName = "administrator-v1")]
//    [FwController(Id:"65DqKhkFbr0G")]
//    public class ControlController : AppDataController
//    {
//        public ControlController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ControlLogic); }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/control/browse 
//        [HttpPost("browse")]
//        [FwControllerMethod(Id:"X0bZScFkD2TT", ActionType: FwControllerActionTypes.View)]
//        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
//        {
//            return await DoBrowseAsync(browseRequest);
//        }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
//        [HttpPost("exportexcelxlsx")]
//        [FwControllerMethod(Id:"u2PvBcxs6Oav", ActionType: FwControllerActionTypes.View)]
//        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
//        {
//            return await DoExportExcelXlsxFileAsync(browseRequest);
//        }
//        //------------------------------------------------------------------------------------ 
//        // GET api/v1/control 
//        [HttpGet]
//        [FwControllerMethod(Id:"BNiKRXunAFz4", ActionType: FwControllerActionTypes.View)]
//        public async Task<ActionResult<IEnumerable<ControlLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
//        {
//            return await DoGetAsync<ControlLogic>(pageno, pagesize, sort);
//        }
//        //------------------------------------------------------------------------------------ 
//        // GET api/v1/control/A0000001 
//        [HttpGet("{id}")]
//        [FwControllerMethod(Id:"4onh2OOXqajh", ActionType: FwControllerActionTypes.View)]
//        public async Task<ActionResult<ControlLogic>> GetOneAsync([FromRoute]string id)
//        {
//            return await DoGetAsync<ControlLogic>(id);
//        }
//        //------------------------------------------------------------------------------------ 
//        // POST api/v1/control 
//        [HttpPost]
//        [FwControllerMethod(Id:"WhslrxSo2Eq8", ActionType: FwControllerActionTypes.Edit)]
//        public async Task<ActionResult<ControlLogic>> PostAsync([FromBody]ControlLogic l)
//        {
//            return await DoPostAsync<ControlLogic>(l);
//        }
//        //------------------------------------------------------------------------------------ 
//        //// DELETE api/v1/control/A0000001 
//        //[HttpDelete("{id}")]
//        //[FwControllerMethod(Id:"anneYdtdLQoV", ActionType: FwControllerActionTypes.Delete)]
//        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
//        //{
//        //    return await <ControlLogic>DoDeleteAsync(id);
//        //}
//        ////------------------------------------------------------------------------------------ 
//    }
//}
