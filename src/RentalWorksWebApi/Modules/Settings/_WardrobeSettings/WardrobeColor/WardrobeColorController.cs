using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeColor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Kc77x9j3t1Cf8")]
    public class WardrobeColorController : AppDataController
    {
        public WardrobeColorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeColorLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"FCCugQbzWWdIV")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"IK2bgXjSg2HEl")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecolor
        [HttpGet]
        [FwControllerMethod(Id:"MhDjti6lUxLc1")]
        public async Task<ActionResult<IEnumerable<WardrobeColorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeColorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobecolor/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"7ggdyMax75deO")]
        public async Task<ActionResult<WardrobeColorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeColorLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobecolor
        [HttpPost]
        [FwControllerMethod(Id:"P4eQpbWzy9QlR")]
        public async Task<ActionResult<WardrobeColorLogic>> PostAsync([FromBody]WardrobeColorLogic l)
        {
            return await DoPostAsync<WardrobeColorLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobecolor/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"OovUuyOuWClQV")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeColorLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
