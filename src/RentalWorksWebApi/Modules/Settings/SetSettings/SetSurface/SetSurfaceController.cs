using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.SetSettings.SetSurface
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Fg5VqZXTcgja2")]
    public class SetSurfaceController : AppDataController
    {
        public SetSurfaceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SetSurfaceLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"pxLw16c3FRdO9", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"tg73D5XU6jwS7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setsurface
        [HttpGet]
        [FwControllerMethod(Id:"fUpF4iqOzsuaq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<SetSurfaceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SetSurfaceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/setsurface/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"DFluxEh4wxWAp", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<SetSurfaceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SetSurfaceLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/setsurface
        [HttpPost]
        [FwControllerMethod(Id:"tVcv6vKO3idAc", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<SetSurfaceLogic>> NewAsync([FromBody]SetSurfaceLogic l)
        {
            return await DoNewAsync<SetSurfaceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/setsurfac/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "aLMjJ0oGBd63s", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<SetSurfaceLogic>> EditAsync([FromRoute] string id, [FromBody]SetSurfaceLogic l)
        {
            return await DoEditAsync<SetSurfaceLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/setsurface/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"G4CdOPdKBkpRa", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SetSurfaceLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
