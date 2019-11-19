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
        [FwControllerMethod(Id:"u6qC8PTeZOah", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"vqorqYAdwvwo", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory
        [HttpGet]
        [FwControllerMethod(Id:"7UihvqInYfBA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomerCategoryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerCategoryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/CustomerCategory/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"MA2bPlOhFORN", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomerCategoryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/CustomerCategory
        [HttpPost]
        [FwControllerMethod(Id:"5gVFRyFJVL0D", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomerCategoryLogic>> NewAsync([FromBody]CustomerCategoryLogic l)
        {
            return await DoNewAsync<CustomerCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/CustomerCategor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "2M7AAA4b7q3pz", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomerCategoryLogic>> EditAsync([FromRoute] string id, [FromBody]CustomerCategoryLogic l)
        {
            return await DoEditAsync<CustomerCategoryLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/CustomerCategory/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"SPtVH2HGBgMe", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomerCategoryLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
