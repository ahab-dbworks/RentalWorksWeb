using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.SalesCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"XS6vdtV5jQTyF")]
    public class SalesCategoryController : AppDataController
    {
        public SalesCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SalesCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iGvCDBc6iCTOs", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"7TQBqR8a0d6nU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory
        [HttpGet]
        [FwControllerMethod(Id:"nKzzlH2X6e461", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SalesCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SalesCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/salescategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"CtaBWOgfnFdio", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SalesCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/salescategory
        [HttpPost]
        [FwControllerMethod(Id:"eQE7mrt1VxdOK", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SalesCategoryLogic>> NewAsync([FromBody]SalesCategoryLogic l)
        {
            return await DoNewAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/salescategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "objnsebcEDwKK", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SalesCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]SalesCategoryLogic l)
        {
            return await DoEditAsync<SalesCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/salescategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"iXj9iGvriNmmc", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SalesCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
