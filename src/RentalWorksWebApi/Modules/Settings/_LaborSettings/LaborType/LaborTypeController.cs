using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborSettings.LaborType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"FGjikpXt4iRf")]
    public class LaborTypeController : AppDataController
    {
        public LaborTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ARyrZZLK2mFk")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"r1Ko65pNImpp")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype
        [HttpGet]
        [FwControllerMethod(Id:"EqIh7gTYzmoR")]
        public async Task<ActionResult<IEnumerable<LaborTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/labortype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"fH68oRcUBPBw")]
        public async Task<ActionResult<LaborTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/labortype
        [HttpPost]
        [FwControllerMethod(Id:"2QI5DhBIaoGe")]
        public async Task<ActionResult<LaborTypeLogic>> PostAsync([FromBody]LaborTypeLogic l)
        {
            return await DoPostAsync<LaborTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/labortype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"qQGw7LTbARvg")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<LaborTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
