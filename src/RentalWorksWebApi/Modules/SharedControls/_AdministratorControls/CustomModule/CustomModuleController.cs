using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.AdministratorControls.CustomModule
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"O5AEYGPZCva3")]
    public class CustomModuleController : AppDataController
    {
        public CustomModuleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomModuleLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/custommodule/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"rrWnLZJuBzz6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"sowZerXs07Fd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/custommodule 
        [HttpGet]
        [FwControllerMethod(Id:"pRYk66IzPbRK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomModuleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomModuleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 

    }
}
