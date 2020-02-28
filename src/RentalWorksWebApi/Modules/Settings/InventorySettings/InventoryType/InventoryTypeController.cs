using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;

namespace WebApi.Modules.Settings.InventorySettings.InventoryType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"aFLFxVNukHJt")]
    public class InventoryTypeController : AppDataController
    {
        public InventoryTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lL61epuHQalZ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"MHyVZ85JI8M8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype
        [HttpGet]
        [FwControllerMethod(Id:"ykR4x0wFMUn7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/inventorytype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ce4lIyWC6DED", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<InventoryTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype
        [HttpPost]
        [FwControllerMethod(Id:"X9DLhKYWzLDt", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryTypeLogic>> NewAsync([FromBody]InventoryTypeLogic l)
        {
            return await DoNewAsync<InventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/inventorytyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "9CWnAlj7NAv2Y", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryTypeLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryTypeLogic l)
        {
            return await DoEditAsync<InventoryTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/inventorytype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"KrYwO2Kkk0pL", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/inventorytype/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "hNnPwcBb32JST", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SortItemsResponse>> SortInventoryTypesAsync([FromBody]SortInventoryTypeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await InventoryTypeFunc.SortInventoryType(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);

                //------------------------------------------------------------------------------------
            }
        }
        //------------------------------------------------------------------------------------
    }
}
