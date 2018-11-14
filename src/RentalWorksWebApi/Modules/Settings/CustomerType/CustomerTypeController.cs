using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CustomerType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"gk8NipmJErWZ")]
    public class CustomerTypeController : AppDataController
    {
        public CustomerTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{A681A1CC-5F38-4C6A-A96A-9B72EC884EB4}")]
        [FwControllerMethod(Id:"T4NFxZgWKzDb")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"ESGfnn1oZwtu")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [Authorize(Policy = "{38EC0AB9-BA14-429F-ACEE-F5F7A9130A7D}")]
        [FwControllerMethod(Id:"EfVwCDSOP4sz")]
        public async Task<ActionResult<IEnumerable<CustomerTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{B8EB8539-A5EE-4E9F-90A8-599BFB548988}")]
        [FwControllerMethod(Id:"royFJSgqPzMH")]
        public async Task<ActionResult<CustomerTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [Authorize(Policy = "{4A6544A8-12A3-4E13-92CB-19207AF1187E}")]
        [FwControllerMethod(Id:"YToToJqHPCsa")]
        public async Task<ActionResult<CustomerTypeLogic>> PostAsync([FromBody]CustomerTypeLogic l)
        {
            return await DoPostAsync<CustomerTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{AEAA52B7-CD39-4EFC-A9AB-54BB373F3A66}")]
        [FwControllerMethod(Id:"GY9LNmcNp57E")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
}
}
