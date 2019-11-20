using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.Unit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"K87j9eupQwohK")]
    public class UnitController : AppDataController
    {
        public UnitController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UnitLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"mRQNnrFEq4sC4", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"WaKzXrTxgfJ9K", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit
        [HttpGet]
        [FwControllerMethod(Id:"Dko4aH3yyIQbp", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<UnitLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnitLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unit/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"d0c7DHG6ilZ8j", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<UnitLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnitLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unit
        [HttpPost]
        [FwControllerMethod(Id:"TvJNFm1HMDXcU", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UnitLogic>> NewAsync([FromBody]UnitLogic l)
        {
            return await DoNewAsync<UnitLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/uni/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "nFbuCqDu2KS1n", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UnitLogic>> EditAsync([FromRoute] string id, [FromBody]UnitLogic l)
        {
            return await DoEditAsync<UnitLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unit/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0VQksUQtkYg9X", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UnitLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
