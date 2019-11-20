using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.HolidaySettings.BlackoutStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"1B2gGaIJglY")]
    public class BlackoutStatusController : AppDataController
    {
        public BlackoutStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BlackoutStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"bxy3ONGlSyk", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"r0qWX7JcPUL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus
        [HttpGet]
        [FwControllerMethod(Id:"tYnzcntGlqo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<BlackoutStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BlackoutStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/blackoutstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"bRtTdiTku8N", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<BlackoutStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BlackoutStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/blackoutstatus
        [HttpPost]
        [FwControllerMethod(Id:"TasAvZCSso8", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<BlackoutStatusLogic>> NewAsync([FromBody]BlackoutStatusLogic l)
        {
            return await DoNewAsync<BlackoutStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/blackoutstatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "fr2a740yF8eCv", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<BlackoutStatusLogic>> EditAsync([FromRoute] string id, [FromBody]BlackoutStatusLogic l)
        {
            return await DoEditAsync<BlackoutStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/blackoutstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"95GiHb7P2C7", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BlackoutStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
