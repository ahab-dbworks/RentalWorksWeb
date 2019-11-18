using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DealType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"jZCS1X5BzeyS")]
    public class DealTypeController : AppDataController
    {
        public DealTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"T2C76lYORyv1")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"janBTZ7FJUdx")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype
        [HttpGet]
        [FwControllerMethod(Id:"A1yFIADuIAMc")]
        public async Task<ActionResult<IEnumerable<DealTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealtype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"BVncSxXlNj4Z")]
        public async Task<ActionResult<DealTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealtype
        [HttpPost]
        [FwControllerMethod(Id:"GKDbyDl9TPLm")]
        public async Task<ActionResult<DealTypeLogic>> PostAsync([FromBody]DealTypeLogic l)
        {
            return await DoPostAsync<DealTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealtype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gEidHhuH1Fx6")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
