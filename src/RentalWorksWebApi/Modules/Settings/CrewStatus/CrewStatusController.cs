using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CrewStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"uW0hAqUv6mDL")]
    public class CrewStatusController : AppDataController
    {
        public CrewStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CrewStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"jytxyhG31DQI")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"cpAG3T8cl9SM")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewstatus
        [HttpGet]
        [FwControllerMethod(Id:"9mz0YTiM9Okm")]
        public async Task<ActionResult<IEnumerable<CrewStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CrewStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/crewstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"AixVcuKCOKHy")]
        public async Task<ActionResult<CrewStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CrewStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/crewstatus
        [HttpPost]
        [FwControllerMethod(Id:"zCrvul9cmmdC")]
        public async Task<ActionResult<CrewStatusLogic>> PostAsync([FromBody]CrewStatusLogic l)
        {
            return await DoPostAsync<CrewStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/crewstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qCpoTdJ5J4TX")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CrewStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
