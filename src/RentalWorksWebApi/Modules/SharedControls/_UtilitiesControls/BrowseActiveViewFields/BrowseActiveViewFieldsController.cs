using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;

namespace WebApi.Modules.UtilitiesControls.BrowseActiveViewFields
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "orE2GTiLSL78u")]
    public class BrowseActiveViewFieldsController : AppDataController
    {
        public BrowseActiveViewFieldsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BrowseActiveViewFieldsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "VPjHMyPIydkp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "7GT4iSXVJRqU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/browseactiveviewfields 
        [HttpGet]
        [FwControllerMethod(Id: "EFqgNmI2uOJX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<BrowseActiveViewFieldsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BrowseActiveViewFieldsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/browseactiveviewfields/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "kBLo5wQO7sRc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<BrowseActiveViewFieldsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BrowseActiveViewFieldsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/browseactiveviewfields 
        [HttpPost]
        [FwControllerMethod(Id: "TY5nOe465Jdd", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<BrowseActiveViewFieldsLogic>> NewAsync([FromBody]BrowseActiveViewFieldsLogic l)
        {
            return await DoNewAsync<BrowseActiveViewFieldsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/browseactiveviewfields/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "X22m4E59yUEuD", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<BrowseActiveViewFieldsLogic>> EditAsync([FromRoute] string id, [FromBody]BrowseActiveViewFieldsLogic l)
        {
            return await DoEditAsync<BrowseActiveViewFieldsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/browseactiveviewfields/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "ASkfPgAgqqNK", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BrowseActiveViewFieldsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
