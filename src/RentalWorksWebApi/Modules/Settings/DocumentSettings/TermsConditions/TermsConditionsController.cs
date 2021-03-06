using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DocumentSettings.TermsConditions
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"lYqC40ZjalGUy")]
    public class TermsConditionsController : AppDataController
    {
        public TermsConditionsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TermsConditionsLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/termsconditions/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"VN5qbnV8xVRKC", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"UBeoO8TLymo13", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/termsconditions
        [HttpGet]
        [FwControllerMethod(Id:"64YwIC3CNIbft", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<TermsConditionsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TermsConditionsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/termsconditions/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"NF0rA4ryw46ld", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<TermsConditionsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TermsConditionsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/termsconditions
        [HttpPost]
        [FwControllerMethod(Id:"9HKA2gwEFiO94", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<TermsConditionsLogic>> NewAsync([FromBody]TermsConditionsLogic l)
        {
            return await DoNewAsync<TermsConditionsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/termscondition/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "xSXKR3Mij1cX1", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<TermsConditionsLogic>> EditAsync([FromRoute] string id, [FromBody]TermsConditionsLogic l)
        {
            return await DoEditAsync<TermsConditionsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/termsconditions/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gZrIOJtUY2Opj", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<TermsConditionsLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
