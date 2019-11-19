using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobePattern
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"3SMTlxYJ2LGH1")]
    public class WardrobePatternController : AppDataController
    {
        public WardrobePatternController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobePatternLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"XBzTtIfPAFVgZ", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"f6YLfzMW4HTif", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern
        [HttpGet]
        [FwControllerMethod(Id:"rBOh7oshuTBR6", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobePatternLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobePatternLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobepattern/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5qRsdDBX7V0uX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobePatternLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobePatternLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobepattern
        [HttpPost]
        [FwControllerMethod(Id:"905S7t2LYfmI6", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobePatternLogic>> NewAsync([FromBody]WardrobePatternLogic l)
        {
            return await DoNewAsync<WardrobePatternLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobepatter/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "BYyYcucL05IFG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobePatternLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobePatternLogic l)
        {
            return await DoEditAsync<WardrobePatternLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobepattern/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"W3MNIb72R6jV5", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobePatternLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
