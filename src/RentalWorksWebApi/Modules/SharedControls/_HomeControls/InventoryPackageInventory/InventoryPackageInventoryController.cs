using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Logic;
using System;

namespace WebApi.Modules.HomeControls.InventoryPackageInventory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"ABL0XJQpsQQo")]
    public class InventoryPackageInventoryController : AppDataController
    {
        public InventoryPackageInventoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryPackageInventoryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hjMyubido3ma", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"rz1vur2Bi7XN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory 
        [HttpGet]
        [FwControllerMethod(Id:"TlB11czcREeK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryPackageInventoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorypackageinventory/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"ApDS0FJu8D48", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryPackageInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory 
        [HttpPost]
        [FwControllerMethod(Id:"EKGGGkcY7321", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> NewAsync([FromBody]InventoryPackageInventoryLogic l)
        {
            return await DoNewAsync<InventoryPackageInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorypackageinventory/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "gIeTitnEyw1wy", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryPackageInventoryLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryPackageInventoryLogic l)
        {
            return await DoEditAsync<InventoryPackageInventoryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorypackageinventory/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"wABYpEfBSzxd", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryPackageInventoryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorypackageinventory/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "2hIGHEjbbv8T2", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SortItemsResponse>> SortInventoryPackageInventoryAsync([FromBody]SortInventoryPackageInventorysRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await InventoryPackageInventoryFunc.SortInventoryPackageInventorys(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
