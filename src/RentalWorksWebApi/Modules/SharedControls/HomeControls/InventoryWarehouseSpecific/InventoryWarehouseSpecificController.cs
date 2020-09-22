using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.BusinessLogic;
using WebApi.Modules.Inventory.Inventory;

namespace WebApi.Modules.HomeControls.InventoryWarehouseSpecific
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "HUmVUurETwRho")]
    public class InventoryWarehouseSpecificController : AppDataController
    {
        public InventoryWarehouseSpecificController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryWarehouseSpecificLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehousespecific/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id: "bcxzZ2wG98E5m", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "OkvJtqXMfuViM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehousespecific
        [HttpGet]
        [FwControllerMethod(Id: "DhAncG7uZsqvV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryWarehouseSpecificLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryWarehouseSpecificLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehousespecific/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "kSddprw8VTlXw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryWarehouseSpecificLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryWarehouseSpecificLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehousespecific
        [HttpPost]
        [FwControllerMethod(Id: "QAgMD3GeciBKq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryWarehouseSpecificLogic>> NewAsync([FromBody]InventoryWarehouseSpecificLogic l)
        {
            //return await DoNewAsync<InventoryWarehouseSpecificLogic>(l);


            InventoryWarehouseSpecificPackageRequest request = new InventoryWarehouseSpecificPackageRequest();
            request.InventoryId = l.InventoryId;
            request.WarehouseId = l.WarehouseId;
            request.IsWarehouseSpecific = true;
            InventoryWarehouseSpecificPackageResponse response = await InventoryFunc.SetWarehouseSpecificPackage(AppConfig, UserSession, request);

            InventoryWarehouseSpecificLogic l2 = new InventoryWarehouseSpecificLogic();
            l2.InventoryId = l.InventoryId;
            l2.WarehouseId = l.WarehouseId;
            await l2.LoadAsync<InventoryWarehouseSpecificLogic>();
            return new OkObjectResult(l2);
        }
        //------------------------------------------------------------------------------------
        //// PUT api/v1/inventorywarehousespecific/A0000001
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "NP5UXdk98OcTi", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<InventoryWarehouseSpecificLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryWarehouseSpecificLogic l)
        //{
        //    return await DoEditAsync<InventoryWarehouseSpecificLogic>(l);
        //}
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorywarehousespecific/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "o7vOmWdbDCd70", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            //return await DoDeleteAsync<InventoryWarehouseSpecificLogic>(id);

            string[] ids = id.Split('~');

            InventoryWarehouseSpecificPackageRequest request = new InventoryWarehouseSpecificPackageRequest();
            //request.InventoryId = l.InventoryId;
            //request.WarehouseId = l.WarehouseId;
            request.IsWarehouseSpecific = false;
            InventoryWarehouseSpecificPackageResponse response = await InventoryFunc.SetWarehouseSpecificPackage(AppConfig, UserSession, request);

            return new OkObjectResult(true);

        }
        //------------------------------------------------------------------------------------
    }
}
