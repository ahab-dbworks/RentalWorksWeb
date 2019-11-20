using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.HomeControls.ContainerWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"4gsBzepUJdWm")]
    public class ContainerWarehouseController : AppDataController
    {
        public ContainerWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerWarehouseLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"sHg0UJP0PBRs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"UVZTgtNlkKHe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containerwarehouse 
        [HttpGet]
        [FwControllerMethod(Id:"NMnVjkIj8cBs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ContainerWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containerwarehouse/A0000001~A0000002
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3V1LyD5DxAzO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ContainerWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse 
        [HttpPost]
        [FwControllerMethod(Id:"ERqWT7c7jXYz", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ContainerWarehouseLogic>> NewAsync([FromBody]ContainerWarehouseLogic l)
        {
            return await DoNewAsync<ContainerWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/containerwarehouse/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "Xhhae5hJIf1pf", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ContainerWarehouseLogic>> EditAsync([FromRoute] string id, [FromBody]ContainerWarehouseLogic l)
        {
            return await DoEditAsync<ContainerWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containerwarehouse/A0000001~A0000002 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"dJ5ZXhH0DSW8", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <ContainerWarehouseLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
