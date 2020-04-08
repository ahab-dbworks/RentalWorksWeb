using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
namespace WebApi.Modules.Utilities.RateUpdateItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "QQwyjnERS0Jx")]
    public class RateUpdateItemController : AppDataController
    {
        public RateUpdateItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateUpdateItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "c4DUy6bgAlPn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "i8emEejBnJC0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdateitem 
        [HttpGet]
        [FwControllerMethod(Id: "IY0vmxRY0LML", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RateUpdateItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateUpdateItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/rateupdateitem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "JtHFU6RLESDt", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RateUpdateItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateUpdateItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/rateupdateitem 
        //[HttpPost]
        //[FwControllerMethod(Id: "", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<RateUpdateItemLogic>> NewAsync([FromBody]RateUpdateItemLogic l)
        //{
        //    return await DoNewAsync<RateUpdateItemLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/rateupdateitem/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "8X830IANQIqY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RateUpdateItemLogic>> EditAsync([FromRoute] string id, [FromBody]RateUpdateItemLogic l)
        {
            return await DoEditAsync<RateUpdateItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/rateupdateitem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<RateUpdateItemLogic>(id);
        //}
        //------------------------------------------------------------------------------------ 
    }
}
