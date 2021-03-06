using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.InventorySettings.InventoryType;

namespace WebApi.Modules.Settings.InventorySettings.Attribute
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Ok4Yh4kdsxk")]
    public class AttributeController : AppDataController
    {
        public AttributeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AttributeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"kA5a5jT22bQ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"LBl422r7YkR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute
        [HttpGet]
        [FwControllerMethod(Id:"Xm8esz2YyCa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<AttributeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AttributeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/attribute/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Tmxx6Pgg5UK", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<AttributeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AttributeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute
        [HttpPost]
        [FwControllerMethod(Id:"4TmA2YCRAah", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<AttributeLogic>> NewAsync([FromBody]AttributeLogic l)
        {
            return await DoNewAsync<AttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/attribut/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "ip9WYDScZ6sXG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<AttributeLogic>> EditAsync([FromRoute] string id, [FromBody]AttributeLogic l)
        {
            return await DoEditAsync<AttributeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/attribute/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"1pSGoEbLpZ3", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AttributeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/attribute/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "7n7AgQygp5ok", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
