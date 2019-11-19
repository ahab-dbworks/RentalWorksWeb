using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerType
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
        [FwControllerMethod(Id:"T4NFxZgWKzDb", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ESGfnn1oZwtu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype
        [HttpGet]
        [FwControllerMethod(Id:"EfVwCDSOP4sz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomerTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customertype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"royFJSgqPzMH", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomerTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customertype
        [HttpPost]
        [FwControllerMethod(Id:"YToToJqHPCsa", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomerTypeLogic>> NewAsync([FromBody]CustomerTypeLogic l)
        {
            return await DoNewAsync<CustomerTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/customertyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "0rxS7504AzDNS", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomerTypeLogic>> EditAsync([FromRoute] string id, [FromBody]CustomerTypeLogic l)
        {
            return await DoEditAsync<CustomerTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customertype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"GY9LNmcNp57E", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomerTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
}
}
