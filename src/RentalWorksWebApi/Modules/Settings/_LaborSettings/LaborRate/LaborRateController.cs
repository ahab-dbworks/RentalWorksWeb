using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborSettings.LaborRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"GRs9mNWBxRw4")]
    public class LaborRateController : AppDataController
    {
        public LaborRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/laborrate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GcZpW5AVdlYX", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"46EFpdFtZ38e", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/laborrate 
        [HttpGet]
        [FwControllerMethod(Id:"PxQR1Sbkcj9Y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<LaborRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborRateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/laborrate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"uK7srQkhvveJ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<LaborRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/laborrate 
        [HttpPost]
        [FwControllerMethod(Id:"B21Z8e2iqqpd", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<LaborRateLogic>> NewAsync([FromBody]LaborRateLogic l)
        {
            return await DoNewAsync<LaborRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/laborrate/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "jLFRU8ZFABSOa", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<LaborRateLogic>> EditAsync([FromRoute] string id, [FromBody]LaborRateLogic l)
        {
            return await DoEditAsync<LaborRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/laborrate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"IELd5mNxuVro", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<LaborRateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
} 
