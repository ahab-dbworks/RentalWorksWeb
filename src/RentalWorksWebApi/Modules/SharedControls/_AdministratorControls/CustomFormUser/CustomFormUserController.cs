using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.AdministratorControls.CustomFormUser
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "nHNdXDBX6m6cp")]
    public class CustomFormUserController : AppDataController
    {
        public CustomFormUserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFormUserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformuser/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "NsKm1SwS3Mjo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformuser/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "B5l2KgRvlVnz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customformuser 
        [HttpGet]
        [FwControllerMethod(Id: "XMW4mqgFmyK2p", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomFormUserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFormUserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customformuser/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "fW60ZtXxzQsf", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomFormUserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFormUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customformuser 
        [HttpPost]
        [FwControllerMethod(Id: "GEwAjdPQd4kS", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomFormUserLogic>> NewAsync([FromBody]CustomFormUserLogic l)
        {
            return await DoNewAsync<CustomFormUserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/customformuser/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "wlTLn3bTp9Du6", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomFormUserLogic>> EditAsync([FromRoute] string id, [FromBody]CustomFormUserLogic l)
        {
            return await DoEditAsync<CustomFormUserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customformuser/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "IQZscu9OBtw9", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomFormUserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
