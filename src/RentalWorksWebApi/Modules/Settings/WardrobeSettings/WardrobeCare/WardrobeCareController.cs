using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeCare
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"DcPDf33MJtiO4")]
    public class WardrobeCareController : AppDataController
    {
        public WardrobeCareController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeCareLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecare/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"oRDdJaw2g1RHM", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"DpOroEkB2BIvC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecare
        [HttpGet]
        [FwControllerMethod(Id:"CbEnQxu7tIZZ2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobeCareLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeCareLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecare/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"vbU2jz0IgNWk4", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobeCareLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeCareLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecare
        [HttpPost]
        [FwControllerMethod(Id:"seKffKtiH0vil", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobeCareLogic>> NewAsync([FromBody]WardrobeCareLogic l)
        {
            return await DoNewAsync<WardrobeCareLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobecar/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "fsusEyNmo7OVw", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobeCareLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobeCareLogic l)
        {
            return await DoEditAsync<WardrobeCareLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecare/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Vps90vV7HpHTn", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeCareLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
