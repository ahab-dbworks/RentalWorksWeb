using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeLabel
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"oPEyg39ExOQhO")]
    public class WardrobeLabelController : AppDataController
    {
        public WardrobeLabelController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeLabelLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobelabel/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"lENznjqLW88vI", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"CKCvFeYxOG82y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobelabel
        [HttpGet]
        [FwControllerMethod(Id:"vWrCRFPxnzsX2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WardrobeLabelLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeLabelLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobelabel/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"FEJBdLL93B8l4", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WardrobeLabelLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeLabelLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobelabel
        [HttpPost]
        [FwControllerMethod(Id:"kBUwj59nhzUvk", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WardrobeLabelLogic>> NewAsync([FromBody]WardrobeLabelLogic l)
        {
            return await DoNewAsync<WardrobeLabelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/wardrobelabe/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "BYSCNBDRD2vVc", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WardrobeLabelLogic>> EditAsync([FromRoute] string id, [FromBody]WardrobeLabelLogic l)
        {
            return await DoEditAsync<WardrobeLabelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobelabel/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"hv7dRtjzh5020", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WardrobeLabelLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
