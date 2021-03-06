using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PresentationSettings.FormDesign
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"er64NLGnsWN7")]
    public class FormDesignController : AppDataController
    {
        public FormDesignController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FormDesignLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"rzQyTMrOuIOL", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"DTEuNR9P4CGQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign
        [HttpGet]
        [FwControllerMethod(Id:"379ztTUuQwiR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FormDesignLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FormDesignLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/formdesign/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"iBq6W1HzBZIJ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FormDesignLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FormDesignLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/formdesign
        [HttpPost]
        [FwControllerMethod(Id:"PU6Q07PSfaFT", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FormDesignLogic>> NewAsync([FromBody]FormDesignLogic l)
        {
            return await DoNewAsync<FormDesignLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/formdesig/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "XC6ZkWo6f7e4T", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FormDesignLogic>> EditAsync([FromRoute] string id, [FromBody]FormDesignLogic l)
        {
            return await DoEditAsync<FormDesignLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/formdesign/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"e6ZDDV8xALp4", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FormDesignLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
