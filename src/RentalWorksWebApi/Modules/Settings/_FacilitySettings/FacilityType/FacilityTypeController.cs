using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilitySettings.FacilityType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"sp3q4geu1RZM")]
    public class FacilityTypeController : AppDataController
    {
        public FacilityTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ndr2gAHvQjB3", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"WRSxPZb0v5iO", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype
        [HttpGet]
        [FwControllerMethod(Id:"KwEATUghLmiv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FacilityTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilitytype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"j9I6mtDok1MS", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<FacilityTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilitytype
        [HttpPost]
        [FwControllerMethod(Id:"Oi2ax1ON7lOI", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FacilityTypeLogic>> NewAsync([FromBody]FacilityTypeLogic l)
        {
            return await DoNewAsync<FacilityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/facilitytyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "hLuAwCcZWy2uG", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FacilityTypeLogic>> EditAsync([FromRoute] string id, [FromBody]FacilityTypeLogic l)
        {
            return await DoEditAsync<FacilityTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilitytype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"vSVG7sWVZhiK", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FacilityTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
