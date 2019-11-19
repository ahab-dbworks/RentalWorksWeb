using FwStandard.AppManager;
﻿using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    //[ApiExplorerSettings(GroupName = "v1")]
    [FwController(Id:"ZbZ8bywECnnE")]
    public class CustomerStatusController : AppDataController
    {
        public CustomerStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomerStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"KzgjpvIVGI1U", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"15HQTKcymqtl", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        [HttpGet]
        [FwControllerMethod(Id:"Im04uc86spWS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<CustomerStatusLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomerStatusLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(CustomerStatusLogic))]
        [SwaggerResponse(200, Type = typeof(CustomerStatusLogic))]
        [FwControllerMethod(Id:"YcelqqpvPUE9", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<CustomerStatusLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomerStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        [FwControllerMethod(Id:"H9LlejpZEsrD", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<CustomerStatusLogic>> NewAsync([FromBody]CustomerStatusLogic l)
        {
            return await DoNewAsync<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/customerstatu/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "tBlMr8XiJfknN", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<CustomerStatusLogic>> EditAsync([FromRoute] string id, [FromBody]CustomerStatusLogic l)
        {
            return await DoEditAsync<CustomerStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"2g1IOr0BjKFm", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomerStatusLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
