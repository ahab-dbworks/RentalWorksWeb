using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Home.InventoryWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"g8sCuKjUVrW1")]
    public class InventoryWarehouseController : AppDataController
    {
        public InventoryWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"u4gGO4k7m7dM")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"J6rnQXi09MU3")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse
        [HttpGet]
        [FwControllerMethod(Id:"IwhOne2eZiEV")]
        public async Task<ActionResult<IEnumerable<InventoryWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorywarehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"1BH7l77QHM7U")]
        public async Task<ActionResult<InventoryWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse
        [HttpPost]
        [FwControllerMethod(Id:"niRzFtvZPut3")]
        public async Task<ActionResult<InventoryWarehouseLogic>> PostAsync([FromBody]InventoryWarehouseLogic l)
        {
            return await DoPostAsync<InventoryWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorywarehouse/many
        [HttpPost("many")]
        [FwControllerMethod(Id: "LfWmFc9AxLtzg")]
        public async Task<List<ActionResult<InventoryWarehouseLogic>>> PostAsync([FromBody]List<InventoryWarehouseLogic> l)
        {
            FwBusinessLogicList l2 = new FwBusinessLogicList();
            l2.AddRange(l);
            return await DoPostAsync<InventoryWarehouseLogic>(l2);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventorywarehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qS1qlnW5sxyS")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
