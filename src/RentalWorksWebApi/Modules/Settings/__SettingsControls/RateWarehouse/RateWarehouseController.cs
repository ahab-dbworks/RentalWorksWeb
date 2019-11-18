using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.RateWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"oVjmeqXtHEJCm")]
    public class RateWarehouseController : AppDataController
    {
        public RateWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/ratewarehouse/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"a22Sl9rz3eXKn")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"tbt6zgBI6nRxG")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/ratewarehouse
        [HttpGet]
        [FwControllerMethod(Id:"uVLT1npYgaSeZ")]
        public async Task<ActionResult<IEnumerable<RateWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/ratewarehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BOd2qFMIjxe0i")]
        public async Task<ActionResult<RateWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/ratewarehouse
        [HttpPost]
        [FwControllerMethod(Id:"Q9JwcijHM4IUt")]
        public async Task<ActionResult<RateWarehouseLogic>> PostAsync([FromBody]RateWarehouseLogic l)
        {
            return await DoPostAsync<RateWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/ratewarehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lEuhw27ObIwK4")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RateWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
