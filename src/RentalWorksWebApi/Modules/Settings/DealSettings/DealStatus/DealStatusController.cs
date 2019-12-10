using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.CustomerSettings.CreditStatus;

namespace WebApi.Modules.Settings.DealSettings.DealStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"CHOTGdFVlnFK")]
    public class DealStatusController : AppDataController
    {
        public DealStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"xe3Iuv5gJEBg", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"iClvZefyHEoF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus
        [HttpGet]
        [FwControllerMethod(Id:"XuLVm4no7I01", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<DealStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/dealstatus/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"M75TRmwTWZOg", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<DealStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus
        [HttpPost]
        [FwControllerMethod(Id:"k4ajzXtOBUUj", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DealStatusLogic>> NewAsync([FromBody]DealStatusLogic l)
        {
            return await DoNewAsync<DealStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/dealstatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "qiW7zEquMNjc7", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DealStatusLogic>> EditAsync([FromRoute] string id, [FromBody]DealStatusLogic l)
        {
            return await DoEditAsync<DealStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/dealstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"IA0YUXIK11X7", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DealStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dealstatus/validatecreditstatus/browse
        [HttpPost("validatecreditstatus/browse")]
        [FwControllerMethod(Id: "PsfyNzBWU6hJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCreditStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CreditStatusLogic>(browseRequest);
        }
    }
}
