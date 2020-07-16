using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.DepartmentInventoryType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "TEiHWtIOkGrX0")]
    public class DepartmentInventoryTypeController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public DepartmentInventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DepartmentInventoryTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentinventorytype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "tFNzAKmanv64D", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentinventorytype/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id: "tFVA7nXeE8MrL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentinventorytype 
        [HttpGet]
        [FwControllerMethod(Id: "TgaTdy3M8225n", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DepartmentInventoryTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DepartmentInventoryTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/departmentinventorytype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "tggAerzPcGU5u", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DepartmentInventoryTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DepartmentInventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/departmentinventorytype 
        [HttpPost]
        [FwControllerMethod(Id: "Tha35jQT4SOtj", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DepartmentInventoryTypeLogic>> NewAsync([FromBody]DepartmentInventoryTypeLogic l)
        {
            return await DoNewAsync<DepartmentInventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/departmentinventorytype/A0000001 
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "thGDlj5PA5GDO", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DepartmentInventoryTypeLogic>> EditAsync([FromRoute] string id, [FromBody]DepartmentInventoryTypeLogic l)
        {
            return await DoEditAsync<DepartmentInventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/departmentinventorytype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "tHwEfJ6zacFoJ", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DepartmentInventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
