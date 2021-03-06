using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorMake
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"fHix04T2Hsc6")]
    public class GeneratorMakeController : AppDataController
    {
        public GeneratorMakeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorMakeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormake/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"x2wR96T7ypcb", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"frAw7Jmqp62L", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormake
        [HttpGet]
        [FwControllerMethod(Id:"AGLmXsfkHtDG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GeneratorMakeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorMakeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatormake/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"jwHjSLUkllAm", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GeneratorMakeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorMakeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatormake
        [HttpPost]
        [FwControllerMethod(Id:"wEiY1jmIsh5f", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GeneratorMakeLogic>> NewAsync([FromBody]GeneratorMakeLogic l)
        {
            return await DoNewAsync<GeneratorMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/generatormak/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "7mJPgFeHdNKyz", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GeneratorMakeLogic>> EditAsync([FromRoute] string id, [FromBody]GeneratorMakeLogic l)
        {
            return await DoEditAsync<GeneratorMakeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatormake/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"3ElE5zF8ufBv", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorMakeLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
