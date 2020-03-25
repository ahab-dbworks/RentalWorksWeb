using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.DataHealth
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "ZtbmCyitBrBe")]
    public class DataHealthController : AppDataController
    {
        public DataHealthController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DataHealthLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/datahealth/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "AGDanxcH9qtP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/datahealth/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "6wvlDZpPNqdw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/datahealth 
        [HttpGet]
        [FwControllerMethod(Id: "su2QMncs171K", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DataHealthLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DataHealthLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/datahealth/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "MbqXnukbZv7A", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DataHealthLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DataHealthLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/datahealth/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "yFuZ3JTiqkPs", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DataHealthLogic>> EditAsync([FromRoute] string id, [FromBody]DataHealthLogic l)
        {
            return await DoEditAsync<DataHealthLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
