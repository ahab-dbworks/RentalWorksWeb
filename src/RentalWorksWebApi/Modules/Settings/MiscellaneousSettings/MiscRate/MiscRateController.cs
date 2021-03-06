using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscellaneousSettings.MiscRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"tINVceJ8JoN7")]
    public class MiscRateController : AppDataController
    {
        public MiscRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/miscrate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Vvz1eA23J10k", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ZMPPB67GY3GV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate 
        [HttpGet]
        [FwControllerMethod(Id:"C7BBtJCm0pNm", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<MiscRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"S8PBGCFhiofY", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<MiscRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/miscrate 
        [HttpPost]
        [FwControllerMethod(Id:"ZS6vjXIf8uLL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<MiscRateLogic>> NewAsync([FromBody]MiscRateLogic l)
        {
            return await DoNewAsync<MiscRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/miscrate/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "H0loYx60lxA6M", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<MiscRateLogic>> EditAsync([FromRoute] string id, [FromBody]MiscRateLogic l)
        {
            return await DoEditAsync<MiscRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/miscrate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"F3c4Lkpej6ur", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MiscRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
