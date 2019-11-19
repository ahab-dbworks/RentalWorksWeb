using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PropsSettings.PropsCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"E793OHd1PFRk4")]
    public class PropsConditionController : AppDataController
    {
        public PropsConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PropsConditionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"mOgcPIhDCvu4H")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"adrQ18nFTZNwH")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition
        [HttpGet]
        [FwControllerMethod(Id:"Jma4qYYgFZ7UU")]
        public async Task<ActionResult<IEnumerable<PropsConditionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PropsConditionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"fr6IyRlPGYGDq")]
        public async Task<ActionResult<PropsConditionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PropsConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition
        [HttpPost]
        [FwControllerMethod(Id:"OEKOYtGFcvmiA")]
        public async Task<ActionResult<PropsConditionLogic>> PostAsync([FromBody]PropsConditionLogic l)
        {
            return await DoPostAsync<PropsConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/propscondition/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"ScuWFNRmSp2vz")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PropsConditionLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
