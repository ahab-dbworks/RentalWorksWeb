using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Agent.OrderManifest;

namespace WebApi.Modules.Agent.Order
{
    [FwControl(Caption: "Order Manifiest", SecurityId: "8uhwXXJ95d3o", ControlType: FwControlTypes.Grid)]
    public partial class OrderController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get Order Manifest
        /// </summary>
        /// <param name="browseRequest"></param>
        /// <returns></returns>
        // POST api/v1/order/{orderId}/manifest/browse 
        [HttpPost("manifest/browse")]
        [FwControllerMethod(Id: "QCdz6hgmh7Dh", FwControllerActionTypes.ControlBrowse, ParentId: "8uhwXXJ95d3o")]
        public async Task<ActionResult<FwJsonDataTable>> ManifestBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Export Manifest to Excel
        /// </summary>
        /// <param name="browseRequest"></param>
        /// <returns></returns>
        // POST api/v1/restrictedperson/{id}/aka/exportexcelxlsx 
        [HttpPost("manifest/exportexcelxlsx")]
        [FwControllerMethod(Id: "C9dUnuLJIb8K", FwControllerActionTypes.ControlBrowse, ParentId: "8uhwXXJ95d3o")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ManifestExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync<OrderManifestLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
