using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Home.InventoryWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class InventoryWarehouseController : AppDataController
    {
        public InventoryWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(pageno, pagesize, sort, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(id, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse
        [HttpPost]
        public async Task<ActionResult<InventoryWarehouseLogic>> PostAsync([FromBody]InventoryWarehouseLogic l)
        {
            return await DoPostAsync<InventoryWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorywarehouse/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(InventoryWarehouseLogic));
        }
        //------------------------------------------------------------------------------------
    }
}