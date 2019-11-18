using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"FjAFN8CLolYu")]
    public class MiscTypeController : AppDataController
    {
        public MiscTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"UpugbUR5c4mn")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"SQUFBMlAkzJr")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype
        [HttpGet]
        [FwControllerMethod(Id:"3dTa41qR5g7G")]
        public async Task<ActionResult<IEnumerable<MiscTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/misctype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"kOqv3lloWaru")]
        public async Task<ActionResult<MiscTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/misctype
        [HttpPost]
        [FwControllerMethod(Id:"PBSsZnZZlstr")]
        public async Task<ActionResult<MiscTypeLogic>> PostAsync([FromBody]MiscTypeLogic l)
        {
            return await DoPostAsync<MiscTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/misctype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"zIoEYUaj2Qgk")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<MiscTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
