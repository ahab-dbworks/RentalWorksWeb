using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorTypeWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"N400cxkXaRDx")]
    public class GeneratorTypeWarehouseController : AppDataController
    {
        public GeneratorTypeWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorTypeWarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"31c3Sz9pGBix", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"mps136kBu0Uc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse
        [HttpGet]
        [FwControllerMethod(Id:"sDAbu7Yn0loz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GeneratorTypeWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortypewarehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"2TJr8QaowTgD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GeneratorTypeWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortypewarehouse
        [HttpPost]
        [FwControllerMethod(Id:"RumpC6huxc1L", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GeneratorTypeWarehouseLogic>> NewAsync([FromBody]GeneratorTypeWarehouseLogic l)
        {
            return await DoNewAsync<GeneratorTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/generatortypewarehous/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "vCZqyiivYV2hX", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GeneratorTypeWarehouseLogic>> EditAsync([FromRoute] string id, [FromBody]GeneratorTypeWarehouseLogic l)
        {
            return await DoEditAsync<GeneratorTypeWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortypewarehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hgOaB4W2hs9Z", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorTypeWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
