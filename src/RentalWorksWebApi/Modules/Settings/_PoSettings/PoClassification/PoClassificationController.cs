using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PoClassification
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"skhmIJOt0Fi0")]
    public class PoClassificationController : AppDataController
    {
        public PoClassificationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoClassificationLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"fqMhLYkM1KBl")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"itu5iLGQqk67")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification
        [HttpGet]
        [FwControllerMethod(Id:"jKMau84rveOO")]
        public async Task<ActionResult<IEnumerable<PoClassificationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoClassificationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/poclassification/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"pjDKtEbzIpd7")]
        public async Task<ActionResult<PoClassificationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoClassificationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/poclassification
        [HttpPost]
        [FwControllerMethod(Id:"Q7ds8LumAMht")]
        public async Task<ActionResult<PoClassificationLogic>> PostAsync([FromBody]PoClassificationLogic l)
        {
            return await DoPostAsync<PoClassificationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/poclassification/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"D5zOOZu0jl4C")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoClassificationLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
