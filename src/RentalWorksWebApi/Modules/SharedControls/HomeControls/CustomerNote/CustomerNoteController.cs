using FwStandard.AppManager;
﻿using System.Collections.Generic;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System;
using System.Threading.Tasks;
using WebApi.Modules.Administrator.User;

namespace WebApi.Modules.HomeControls.CustomerNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"6AHfzr9WBEW9")]
    public class CustomerNoteController : AppDataController
    {
        public CustomerNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerNoteLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"DpFKT80eBP5e", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"6aVb9v4Oev3j", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote
        [HttpGet]
        [FwControllerMethod(Id:"fA2W8CUbB7H8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomerNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customernote/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"RlJM6t7oBWcK", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<CustomerNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote
        [HttpPost]
        [FwControllerMethod(Id:"TPzmtyJRciRO", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomerNoteLogic>> NewAsync([FromBody]CustomerNoteLogic l)
        {
            return await DoNewAsync<CustomerNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/customernot/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "nEFDjApz39aQq", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomerNoteLogic>> EditAsync([FromRoute] string id, [FromBody]CustomerNoteLogic l)
        {
            return await DoEditAsync<CustomerNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customernote/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"acHd3VbO3Ei5", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomerNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customernote/validatenotesby/browse
        [HttpPost("validatenotesby/browse")]
        [FwControllerMethod(Id: "28DMFerIzX5j", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateNotesByBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
