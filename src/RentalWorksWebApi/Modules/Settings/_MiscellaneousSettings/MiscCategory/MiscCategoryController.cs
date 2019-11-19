using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscellaneousSettings.MiscCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"BRtP4O9fieRK")]
    public class MiscCategoryController : AppDataController
    {
        public MiscCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GZTmInZm9g7O", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"XC7pBZkJD34L", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory
        [HttpGet]
        [FwControllerMethod(Id:"rzJVIzfuFh2C", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<MiscCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misccategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ei3iW4CdY3sp", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<MiscCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misccategory
        [HttpPost]
        [FwControllerMethod(Id:"l0mqjRHW4Ak3", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<MiscCategoryLogic>> NewAsync([FromBody]MiscCategoryLogic l)
        {
            return await DoNewAsync<MiscCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/misccategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "fFJmgctFYBGKh", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<MiscCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]MiscCategoryLogic l)
        {
            return await DoEditAsync<MiscCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misccategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OrUhIoJ541aC", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MiscCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
