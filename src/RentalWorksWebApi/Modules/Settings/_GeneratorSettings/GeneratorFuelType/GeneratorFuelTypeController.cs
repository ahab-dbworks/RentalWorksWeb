using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.GeneratorFuelType;

namespace WebApi.Modules.Settings.GeneratorSettings.GeneratorFuelType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"WP4ewzQGUV8U")]
    public class GeneratorFuelTypeController : AppDataController
    {
        public GeneratorFuelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneratorFuelTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"Msl0CJCVzzTq", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"KQXphoMHr55U", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype
        [HttpGet]
        [FwControllerMethod(Id:"KrOtiviuNxFY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<GeneratorFuelTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/generatorfueltype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"2EdEa4SaQShn", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<GeneratorFuelTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<GeneratorFuelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generatorfueltype
        [HttpPost]
        [FwControllerMethod(Id:"yrwXq1fu82zO", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<GeneratorFuelTypeLogic>> NewAsync([FromBody]GeneratorFuelTypeLogic l)
        {
            return await DoNewAsync<GeneratorFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/generatorfueltyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "9kIzlHP2HmzZT", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<GeneratorFuelTypeLogic>> EditAsync([FromRoute] string id, [FromBody]GeneratorFuelTypeLogic l)
        {
            return await DoEditAsync<GeneratorFuelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/generatorfueltype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0Y6S4a1XLPPa", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<GeneratorFuelTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
