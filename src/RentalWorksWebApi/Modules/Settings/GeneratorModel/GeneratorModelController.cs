using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorModel
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"CFnh3uxNiWZy")]
    public class GeneratorModelController : AppDataController
    {
        public GeneratorModelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorModelLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormodel/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QGgKJnrEtcJG")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"wYHXIwAUg6Kn")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormodel
        [HttpGet]
        [FwControllerMethod(Id:"I2tbjC68Ti24")]
        public async Task<ActionResult<IEnumerable<GeneratorModelLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorModelLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormodel/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"EjsE8DuxLozX")]
        public async Task<ActionResult<GeneratorModelLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorModelLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormodel
        [HttpPost]
        [FwControllerMethod(Id:"dLD3GBm4V0Uf")]
        public async Task<ActionResult<GeneratorModelLogic>> PostAsync([FromBody]GeneratorModelLogic l)
        {
            return await DoPostAsync<GeneratorModelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatormodel/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8ragFDAQ17hW")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorModelLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
