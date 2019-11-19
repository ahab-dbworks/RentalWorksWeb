using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"mUQp7GqmQlaR")]
    public class GeneratorTypeController : AppDataController
    {
        public GeneratorTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"OOOyCPaiBCjj")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Wsb1kgC6QO2S")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype
        [HttpGet]
        [FwControllerMethod(Id:"7LadFZQWdmNP")]
        public async Task<ActionResult<IEnumerable<GeneratorTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatortype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"DiLpR82Vl7Jz")]
        public async Task<ActionResult<GeneratorTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatortype
        [HttpPost]
        [FwControllerMethod(Id:"eX4udmt8J3hF")]
        public async Task<ActionResult<GeneratorTypeLogic>> PostAsync([FromBody]GeneratorTypeLogic l)
        {
            return await DoPostAsync<GeneratorTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatortype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3KwJCwQCGOPx")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
