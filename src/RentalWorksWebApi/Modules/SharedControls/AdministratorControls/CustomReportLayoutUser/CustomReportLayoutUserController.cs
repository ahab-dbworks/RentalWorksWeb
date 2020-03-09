using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.SharedControls.CustomReportLayoutUser
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "JjgsAURBr00RK")]
    public class CustomReportLayoutUserController : AppDataController
    {
        public CustomReportLayoutUserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomReportLayoutUserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "OKNDtI1hzzqUy")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "9UBxb5wbqpCpb")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutuser 
        [HttpGet]
        [FwControllerMethod(Id: "62LgNhS0XJfLG")]
        public async Task<ActionResult<IEnumerable<CustomReportLayoutUserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomReportLayoutUserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customreportlayoutuser/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "TN68b8dgR8ZkR")]
        public async Task<ActionResult<CustomReportLayoutUserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomReportLayoutUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customreportlayoutuser 
        [HttpPost]
        [FwControllerMethod(Id: "gQA0onWYAnQtv")]
        public async Task<ActionResult<CustomReportLayoutUserLogic>> PostAsync([FromBody]CustomReportLayoutUserLogic l)
        {
            return await DoPostAsync<CustomReportLayoutUserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customreportlayoutuser/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "ipRy905cZR12T")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomReportLayoutUserLogic>(id);
        }
        //------------------------------------------------------------------------------------  
        // POST api/v1/customreportlayoutuser/validateuser/browse
        [HttpPost("validateuser/browse")]
        [FwControllerMethod(Id: "2Cm8T07KIppI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
