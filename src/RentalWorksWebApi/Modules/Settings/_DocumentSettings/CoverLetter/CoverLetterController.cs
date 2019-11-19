using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DocumentSettings.CoverLetter
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ejyCz527IQCS")]
    public class CoverLetterController : AppDataController
    {
        public CoverLetterController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CoverLetterLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QAlScS9ctY0j", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qKeN7PUu4mcG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter
        [HttpGet]
        [FwControllerMethod(Id:"bcja7USfqmzx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CoverLetterLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CoverLetterLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/coverletter/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"PnditdoTGCcX", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CoverLetterLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CoverLetterLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/coverletter
        [HttpPost]
        [FwControllerMethod(Id:"KNluq573AJgA", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CoverLetterLogic>> NewAsync([FromBody]CoverLetterLogic l)
        {
            return await DoNewAsync<CoverLetterLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/coverlette/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "lFnsw6GWLcM4a", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CoverLetterLogic>> EditAsync([FromRoute] string id, [FromBody]CoverLetterLogic l)
        {
            return await DoEditAsync<CoverLetterLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/coverletter/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"MBjxcC9i3Ce7", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CoverLetterLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
