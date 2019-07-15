using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetOpening
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
        [FwControllerMethod(Id:"kFXjtkPyKWYP0")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"0UlkmNBmeUBBH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening
        [HttpGet]
        [FwControllerMethod(Id:"GNec6iIBS5yho")]
        public async Task<ActionResult<IEnumerable<SetOpeningLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetOpeningLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setopening/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FYbNR7FoiaAqu")]
        public async Task<ActionResult<SetOpeningLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetOpeningLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setopening
        [HttpPost]
        [FwControllerMethod(Id:"fJ28PxCmvhnBM")]
        public async Task<ActionResult<SetOpeningLogic>> PostAsync([FromBody]SetOpeningLogic l)
        {
            return await DoPostAsync<SetOpeningLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setopening/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OU8Brzv5qX3ec")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SetOpeningLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
