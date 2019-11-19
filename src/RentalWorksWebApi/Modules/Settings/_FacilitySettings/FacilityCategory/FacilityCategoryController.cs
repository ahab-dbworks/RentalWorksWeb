using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"YA1ynwQcq11")]
    public class FacilityCategoryController : AppDataController
    {
        public FacilityCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Qk4sbICH5dm", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"PqutOqGnl6A", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory
        [HttpGet]
        [FwControllerMethod(Id:"0jTgMkHyFeI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FacilityCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitycategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"TerqcSFF9sD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FacilityCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitycategory
        [HttpPost]
        [FwControllerMethod(Id:"89Tv8xbkMtp", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FacilityCategoryLogic>> NewAsync([FromBody]FacilityCategoryLogic l)
        {
            return await DoNewAsync<FacilityCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/facilitycategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "jtIGbvDe27uUB", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FacilityCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]FacilityCategoryLogic l)
        {
            return await DoEditAsync<FacilityCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitycategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"82eyifH2g7K", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
