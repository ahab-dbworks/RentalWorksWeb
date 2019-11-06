using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.LaborCategory
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
        [FwControllerMethod(Id:"YpVjQnjkZDmB")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"KlWMsgTmGQmD")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory
        [HttpGet]
        [FwControllerMethod(Id:"owr1K0nmNsPV")]
        public async Task<ActionResult<IEnumerable<LaborCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<LaborCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/laborcategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"Ghz7LedIPu1r")]
        public async Task<ActionResult<LaborCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<LaborCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/laborcategory
        [HttpPost]
        [FwControllerMethod(Id:"cqqPj4q421iq")]
        public async Task<ActionResult<LaborCategoryLogic>> PostAsync([FromBody]LaborCategoryLogic l)
        {
            return await DoPostAsync<LaborCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/laborcategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lxqvsNDVsc6m")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<LaborCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
