using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerCategory
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HC4q49WUI1NW")]
    public class CustomerCategoryController : AppDataController
    {
        public CustomerCategoryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerCategoryLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"u6qC8PTeZOah")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"vqorqYAdwvwo")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory
        [HttpGet]
        [FwControllerMethod(Id:"7UihvqInYfBA")]
        public async Task<ActionResult<IEnumerable<CustomerCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"MA2bPlOhFORN")]
        public async Task<ActionResult<CustomerCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory
        [HttpPost]
        [FwControllerMethod(Id:"5gVFRyFJVL0D")]
        public async Task<ActionResult<CustomerCategoryLogic>> PostAsync([FromBody]CustomerCategoryLogic l)
        {
            return await DoPostAsync<CustomerCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/CustomerCategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SPtVH2HGBgMe")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomerCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
