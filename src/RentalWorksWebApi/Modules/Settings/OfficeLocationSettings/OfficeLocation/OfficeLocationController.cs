using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.RateType;
using WebApi.Modules.Settings.PoSettings.PoType;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"yZhqRrXdTEvN")]
    public class OfficeLocationController : AppDataController
    {
        public OfficeLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OfficeLocationLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/officelocation/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ciKUpRcMkWvL", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/officelocation/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"X5zKPFTQcXFh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/officelocation
        [HttpGet]
        [FwControllerMethod(Id:"2Q8uVuiEvufJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<OfficeLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OfficeLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/officelocation/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"WvFQsN6sMTXV", ActionType: FwControllerActionTypes.View, ValidateSecurityGroup: false)]
        public async Task<ActionResult<OfficeLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OfficeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/officelocation
        [HttpPost]
        [FwControllerMethod(Id:"cT02J0bWYX6v", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<OfficeLocationLogic>> NewAsync([FromBody]OfficeLocationLogic l)
        {
            return await DoNewAsync<OfficeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/officelocation/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "T1If2gPyd8her", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<OfficeLocationLogic>> EditAsync([FromRoute] string id, [FromBody]OfficeLocationLogic l)
        {
            return await DoEditAsync<OfficeLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/officelocation/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"YvPKiTjiJyO9", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OfficeLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/officelocation/validateratetype/browse
        [HttpPost("validateratetype/browse")]
        [FwControllerMethod(Id: "7SNMzCvasGuc", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRateTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RateTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/officelocation/validatedefaultpurchasepotype/browse
        [HttpPost("validatedefaultpurchasepotype/browse")]
        [FwControllerMethod(Id: "DdU8FkA8ypoa", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDefaultPurchasePoTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<PoTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
    }
}
