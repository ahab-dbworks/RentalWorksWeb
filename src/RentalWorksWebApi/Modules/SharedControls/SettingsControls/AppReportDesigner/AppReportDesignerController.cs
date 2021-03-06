using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.AppReportDesigner
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"zCgMN4Sp4wR")]
    public class AppReportDesignerController : AppDataController
    {
        public AppReportDesignerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AppReportDesignerLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/appreportdesigner/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"PiB72ic2GSS", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"9mXXAFxyW5E", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/appreportdesigner 
        [HttpGet]
        [FwControllerMethod(Id:"r82t8owa86k", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AppReportDesignerLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AppReportDesignerLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/appreportdesigner/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"AiVBLDgkb41", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<AppReportDesignerLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AppReportDesignerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/appreportdesigner 
        [HttpPost]
        [FwControllerMethod(Id:"IjvcfLcr88p", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AppReportDesignerLogic>> NewAsync([FromBody]AppReportDesignerLogic l)
        {
            return await DoNewAsync<AppReportDesignerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/appreportdesigner/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "mGEiFmxE2oZYF", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AppReportDesignerLogic>> EditAsync([FromRoute] string id, [FromBody]AppReportDesignerLogic l)
        {
            return await DoEditAsync<AppReportDesignerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/appreportdesigner/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"O5oQEjJexUj", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AppReportDesignerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
