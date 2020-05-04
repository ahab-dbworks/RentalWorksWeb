using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Settings.DepartmentSettings.Department;

namespace WebApi.Modules.Settings.TemplateSettings.Template
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"74uXyH7jXXbZM")]
    public class TemplateController : AppDataController
    {
        public TemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"E4bGZf3GhckKT", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"QKvpiTn4OyzQU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template 
        [HttpGet]
        [FwControllerMethod(Id:"q1VbZFAgHkZaZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<TemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TemplateLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/template/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"0Xv0F2r9jmBJZ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<TemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template 
        [HttpPost]
        [FwControllerMethod(Id:"5pRKdQ69h2mLK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<TemplateLogic>> NewAsync([FromBody]TemplateLogic l)
        {
            return await DoNewAsync<TemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/template/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "8D1I6U9tO0Het", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<TemplateLogic>> EditAsync([FromRoute] string id, [FromBody]TemplateLogic l)
        {
            return await DoEditAsync<TemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/template/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"MbDonOp1dphBB", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<TemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/template/validatederate/browse
        [HttpPost("validatederate/browse")]
        [FwControllerMethod(Id: "aoRKyU4qJcC4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/template/validatewarehouse/browse
        [HttpPost("validatewarehouse/browse")]
        [FwControllerMethod(Id: "N39K94aXpzBp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/template/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "eCWs2P3N83Vw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
    }
}
