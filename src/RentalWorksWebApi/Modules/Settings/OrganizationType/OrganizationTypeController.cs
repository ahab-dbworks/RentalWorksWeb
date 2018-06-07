﻿using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.OrganizationType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrganizationTypeController : AppDataController
    {
        public OrganizationTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrganizationTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrganizationTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<OrganizationTypeLogic>(pageno, pagesize, sort, typeof(OrganizationTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<OrganizationTypeLogic>(id, typeof(OrganizationTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]OrganizationTypeLogic l)
        {
            return await DoPostAsync<OrganizationTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/organizationtype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(OrganizationTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}