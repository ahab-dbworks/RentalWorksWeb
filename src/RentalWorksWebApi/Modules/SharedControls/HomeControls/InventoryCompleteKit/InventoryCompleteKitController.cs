using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Modules.HomeControls.InventoryCompleteKit
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "gflkb5sQf7it")]
    public class InventoryCompleteKitController : AppDataController
    {
        public InventoryCompleteKitController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryCompleteKitLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompletekit/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "ogsIQMxKcomk", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "5pkBYsKeb51u", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompletekit 
        [HttpGet]
        [FwControllerMethod(Id: "G9te4FThbZqe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryCompleteKitLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventorycompletekit/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "RFlGGO23Acy5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryCompleteKitLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryCompleteKitLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorycompletekit/createcomplete/A0000001
        [HttpPost("createcomplete/{id}")]
        [FwControllerMethod(Id: "t52dzqYU3K2", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<CreateCompleteResponse>> CreateComplete([FromRoute]string id)
        {
            try
            {
                CreateCompleteResponse response = await InventoryCompleteKitFunc.CreateComplete(AppConfig, UserSession, id);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------       
    }
}
