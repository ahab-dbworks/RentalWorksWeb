using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.GeneratorWatts
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"D2Z3jlFgx8Es")]
    public class GeneratorWattsController : AppDataController
    {
        public GeneratorWattsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorWattsLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"UJxdD5U2Icro")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"1DlwGvDKOO9J")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts
        [HttpGet]
        [FwControllerMethod(Id:"btedXKcu5gNo")]
        public async Task<ActionResult<IEnumerable<GeneratorWattsLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<GeneratorWattsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorwatts/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"TzMA0Db3S0my")]
        public async Task<ActionResult<GeneratorWattsLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<GeneratorWattsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorwatts
        [HttpPost]
        [FwControllerMethod(Id:"HE0oB0KwhLkP")]
        public async Task<ActionResult<GeneratorWattsLogic>> PostAsync([FromBody]GeneratorWattsLogic l)
        {
            return await DoPostAsync<GeneratorWattsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorwatts/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"MJm1LT5dsNWQ")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<GeneratorWattsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
