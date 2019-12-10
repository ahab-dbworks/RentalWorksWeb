using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.LaborSettings.LaborType;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.LaborSettings.LaborCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"nJIiZsDNxc83")]
    public class LaborCategoryController : AppDataController
    {
        public LaborCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LaborCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/laborcategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"YpVjQnjkZDmB", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"KlWMsgTmGQmD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory
        [HttpGet]
        [FwControllerMethod(Id:"owr1K0nmNsPV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<LaborCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ghz7LedIPu1r", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<LaborCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/laborcategory
        [HttpPost]
        [FwControllerMethod(Id:"cqqPj4q421iq", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<LaborCategoryLogic>> NewAsync([FromBody]LaborCategoryLogic l)
        {
            return await DoNewAsync<LaborCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/laborcategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "F7MipDVgIADq2", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<LaborCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]LaborCategoryLogic l)
        {
            return await DoEditAsync<LaborCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/laborcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lxqvsNDVsc6m", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<LaborCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatelabortype/browse
        [HttpPost("validatelabortype/browse")]
        [FwControllerMethod(Id: "ecVm382ssCSI", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateprofitandlosscategory/browse
        [HttpPost("validateprofitandlosscategory/browse")]
        [FwControllerMethod(Id: "HgB94bdPFbLy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProfitAndLossCategoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborCategoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "rGEpVl4X8kOJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "LfJepKpPfIN7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/settings/validateexpenseaccount/browse
        [HttpPost("validateexpenseaccount/browse")]
        [FwControllerMethod(Id: "O4xfU9DJJmY0", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
