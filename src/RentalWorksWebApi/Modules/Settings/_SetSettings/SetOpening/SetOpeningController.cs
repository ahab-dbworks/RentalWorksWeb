using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetSettings.SetOpening
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gwzYE66lX9myO")]
    public class SetOpeningController : AppDataController
    {
        public SetOpeningController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SetOpeningLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"kFXjtkPyKWYP0", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"0UlkmNBmeUBBH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening
        [HttpGet]
        [FwControllerMethod(Id:"GNec6iIBS5yho", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SetOpeningLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetOpeningLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FYbNR7FoiaAqu", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SetOpeningLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetOpeningLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening
        [HttpPost]
        [FwControllerMethod(Id:"fJ28PxCmvhnBM", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SetOpeningLogic>> NewAsync([FromBody]SetOpeningLogic l)
        {
            return await DoNewAsync<SetOpeningLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/setopenin/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "tkfaJq9jdbX69", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SetOpeningLogic>> EditAsync([FromRoute] string id, [FromBody]SetOpeningLogic l)
        {
            return await DoEditAsync<SetOpeningLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setopening/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OU8Brzv5qX3ec", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SetOpeningLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
