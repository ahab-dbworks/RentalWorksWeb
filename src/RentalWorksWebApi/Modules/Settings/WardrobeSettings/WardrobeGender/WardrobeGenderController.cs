using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeGender
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"oZl62v243hmoY")]
    public class WardrobeGenderController : AppDataController
    {
        public WardrobeGenderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeGenderLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Qx1RJH8Wqm3iX", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ufTfVfrVlGIFy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender
        [HttpGet]
        [FwControllerMethod(Id:"oimyDIBwP1TID", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobeGenderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeGenderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobegender/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FtNEx8boapOE4", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobeGenderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeGenderLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobegender
        [HttpPost]
        [FwControllerMethod(Id:"udG3LMrle2Jx9", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobeGenderLogic>> NewAsync([FromBody]WardrobeGenderLogic l)
        {
            return await DoNewAsync<WardrobeGenderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobegende/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "kJ4yNCJ6MdMXc", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobeGenderLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobeGenderLogic l)
        {
            return await DoEditAsync<WardrobeGenderLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobegender/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"orst79gLXDlkX", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeGenderLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
