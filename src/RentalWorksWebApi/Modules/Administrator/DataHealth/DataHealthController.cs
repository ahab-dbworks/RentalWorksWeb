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
        // GET api/v1/datahealth/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "0NhDUgf0OlYyx", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Critical", RwGlobals.DATA_HEALTH_SEVERITY_CRITICAL_COLOR);
            legend.Add("High", RwGlobals.DATA_HEALTH_SEVERITY_HIGH_COLOR);
            legend.Add("Medium", RwGlobals.DATA_HEALTH_SEVERITY_MEDIUM_COLOR);
            legend.Add("Low", RwGlobals.DATA_HEALTH_SEVERITY_LOW_COLOR);
            legend.Add("Warning", RwGlobals.DATA_HEALTH_SEVERITY_WARNING_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
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
