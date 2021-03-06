using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Logic;
using System;

namespace WebApi.Modules.HomeControls.GeneralItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"UgfInM2AmF6B")]
    public class GeneralItemController : AppDataController
    {
        public GeneralItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneralItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/generalitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"VVqLMiJ2Vggt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/generalitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ce6O0nck5e5t", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/generalitem/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "9sKZjhXvCLHME", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SortItemsResponse>> SortGeneralItems([FromBody]SortGeneralItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await GeneralItemFunc.SortGeneralItems(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);

                //------------------------------------------------------------------------------------
            }
        }
    }
}
