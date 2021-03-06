using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.VendorSettings.OrganizationType
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
        [FwControllerMethod(Id:"81dJNm0x36vq", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"tXq539vesCYr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype
        [HttpGet]
        [FwControllerMethod(Id:"lW0NjTL4z9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OrganizationTypeLogic>>> GetManyAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<OrganizationTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/organizationtype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FyMR9nQF5dZ4", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<OrganizationTypeLogic>> GetOneAsync(string id)
        {
            return await DoGetAsync<OrganizationTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/organizationtype
        [HttpPost]
        [FwControllerMethod(Id:"APug6UtlUTM2", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OrganizationTypeLogic>> NewAsync([FromBody]OrganizationTypeLogic l)
        {
            return await DoNewAsync<OrganizationTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/organizationtyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "uvOcFAE0w6Qqj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OrganizationTypeLogic>> EditAsync([FromRoute] string id, [FromBody]OrganizationTypeLogic l)
        {
            return await DoEditAsync<OrganizationTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/organizationtype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"wFat1JRGcNxN", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return await DoDeleteAsync<OrganizationTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
