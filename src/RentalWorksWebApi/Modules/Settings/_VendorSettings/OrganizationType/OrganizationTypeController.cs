using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.OrganizationType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ENv2O3MbwKrI")]
    public class OrganizationTypeController : AppDataController
    {
        public OrganizationTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrganizationTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"81dJNm0x36vq")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"tXq539vesCYr")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype
        [HttpGet]
        [FwControllerMethod(Id:"lW0NjTL4z9")]
        public async Task<ActionResult<IEnumerable<OrganizationTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<OrganizationTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FyMR9nQF5dZ4")]
        public async Task<ActionResult<OrganizationTypeLogic>> GetAsync(string id)
        {
            return await DoGetAsync<OrganizationTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype
        [HttpPost]
        [FwControllerMethod(Id:"APug6UtlUTM2")]
        public async Task<ActionResult<OrganizationTypeLogic>> PostAsync([FromBody]OrganizationTypeLogic l)
        {
            return await DoPostAsync<OrganizationTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/organizationtype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"wFat1JRGcNxN")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<OrganizationTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
