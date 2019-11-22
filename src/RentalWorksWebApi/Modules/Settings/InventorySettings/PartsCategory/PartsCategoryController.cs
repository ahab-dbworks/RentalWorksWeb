using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.PartsCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"aSzlwy6XYMSV")]
    public class PartsCategoryController : AppDataController
    {
        public PartsCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PartsCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"fFMSdQdz6EtM", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"BufRgr9NF2vo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory
        [HttpGet]
        [FwControllerMethod(Id:"QreTs0Crs444", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PartsCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PartsCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/partscategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"gKXhXrk1zsd5", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PartsCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PartsCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/partscategory
        [HttpPost]
        [FwControllerMethod(Id:"n1OeLEBGCTTv", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PartsCategoryLogic>> NewAsync([FromBody]PartsCategoryLogic l)
        {
            return await DoNewAsync<PartsCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/partscategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "GFKWzKoiW7jKP", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PartsCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]PartsCategoryLogic l)
        {
            return await DoEditAsync<PartsCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/partscategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"pzQKEksyLiEu", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PartsCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}