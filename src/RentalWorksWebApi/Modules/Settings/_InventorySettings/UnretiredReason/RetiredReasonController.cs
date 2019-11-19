using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.InventorySettings.UnretiredReason
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"0SWJ0HNGoioxe")]
    public class UnretiredReasonController : AppDataController
    {
        public UnretiredReasonController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UnretiredReasonLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"761JE0CdJJqeN")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"mi4D855tcI4G5")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unretiredreason
        [HttpGet]
        [FwControllerMethod(Id:"vYc4RIiXVGNvR")]
        public async Task<ActionResult<IEnumerable<UnretiredReasonLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UnretiredReasonLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/unretiredreason/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"qLK8k090cnMtb")]
        public async Task<ActionResult<UnretiredReasonLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UnretiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/unretiredreason
        [HttpPost]
        [FwControllerMethod(Id:"92RpqEqYcjazO")]
        public async Task<ActionResult<UnretiredReasonLogic>> PostAsync([FromBody]UnretiredReasonLogic l)
        {
            return await DoPostAsync<UnretiredReasonLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/unretiredreason/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3dkxSsl06CzTq")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UnretiredReasonLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
