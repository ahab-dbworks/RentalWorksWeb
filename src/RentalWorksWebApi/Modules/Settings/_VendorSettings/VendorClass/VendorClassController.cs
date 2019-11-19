using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.VendorSettings.VendorClass
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"EH6T4hlMVhYxq")]
    public class VendorClassController : AppDataController
    {
        public VendorClassController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorClassLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"XN29mz8bvy7rn", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"f3c9aEevD6QKs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass
        [HttpGet]
        [FwControllerMethod(Id:"B35mATR0Upp43", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VendorClassLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorClassLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorclass/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"M82PBPwcsOJsZ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VendorClassLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorClassLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorclass
        [HttpPost]
        [FwControllerMethod(Id:"bw6ImG4S2HDaa", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VendorClassLogic>> NewAsync([FromBody]VendorClassLogic l)
        {
            return await DoNewAsync<VendorClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vendorclas/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "YLhX6L6hEETrF", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VendorClassLogic>> EditAsync([FromRoute] string id, [FromBody]VendorClassLogic l)
        {
            return await DoEditAsync<VendorClassLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorclass/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"FoaTCvXdjlTAy", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VendorClassLogic>(id);
        }
        //------------------------------------------------------------------------------------
}
}
