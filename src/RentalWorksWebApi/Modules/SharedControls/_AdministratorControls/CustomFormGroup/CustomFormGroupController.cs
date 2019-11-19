using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.AdministratorControls.CustomFormGroup
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "11txpzVKVGi2")]
    public class CustomFormGroupController : AppDataController
    {
        public CustomFormGroupController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFormGroupLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformgroup/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "EbgBVyEmJjb8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformgroup/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "BINtb1PjRFnuJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customformgroup 
        [HttpGet]
        [FwControllerMethod(Id: "ksm4EdpWEIlP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomFormGroupLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFormGroupLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customformgroup/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "hEP97F7gZTtS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomFormGroupLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFormGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformgroup 
        [HttpPost]
        [FwControllerMethod(Id: "ogvXUP6wJjkhh", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomFormGroupLogic>> NewAsync([FromBody]CustomFormGroupLogic l)
        {
            return await DoNewAsync<CustomFormGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/customformgroup/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "2zi6FOI8hH4hx", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomFormGroupLogic>> EditAsync([FromRoute] string id, [FromBody]CustomFormGroupLogic l)
        {
            return await DoEditAsync<CustomFormGroupLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customformgroup/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "S5OEQ9jQgwsS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomFormGroupLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
