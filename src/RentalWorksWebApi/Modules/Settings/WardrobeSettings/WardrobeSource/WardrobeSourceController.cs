using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeSource
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ngguXevaaonRp")]
    public class WardrobeSourceController : AppDataController
    {
        public WardrobeSourceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeSourceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ICuq2QRVLRHo4", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"UbQeN6GlnQY3u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource
        [HttpGet]
        [FwControllerMethod(Id:"13lAes6WX7Iqi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobeSourceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeSourceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobesource/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4c6ccCxM1z9xO", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobeSourceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeSourceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobesource
        [HttpPost]
        [FwControllerMethod(Id:"CP1F2C61SyFpM", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobeSourceLogic>> NewAsync([FromBody]WardrobeSourceLogic l)
        {
            return await DoNewAsync<WardrobeSourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobesourc/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "Hap92mGdSUXBh", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobeSourceLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobeSourceLogic l)
        {
            return await DoEditAsync<WardrobeSourceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobesource/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"6gSRksMbuNxDC", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeSourceLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
