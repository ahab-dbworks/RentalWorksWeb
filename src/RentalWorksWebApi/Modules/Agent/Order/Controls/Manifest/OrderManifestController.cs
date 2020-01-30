using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using System;
using WebApi.Data;

namespace WebApi.Modules.Agent.Order
{
    [FwControl(Caption: "Order Manifiest", SecurityId: "8uhwXXJ95d3o", ControlType: FwControlTypes.Grid)]
    public partial class OrderController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Get Order Manifest
        /// </summary>
        /// <param name="OrderId">A unique identifier for Order Manifest</param>
        /// <param name="browseRequest"></param>
        /// <returns></returns>
        // POST api/v1/order/{orderId}/manifest/browse 
        [HttpPost("{orderid}/manifest/browse")]
        [FwControllerMethod(Id: "QCdz6hgmh7Dh", FwControllerActionTypes.ControlBrowse, ParentId: "8uhwXXJ95d3o")]
        public async Task<ActionResult<FwJsonDataTable>> ManifestBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        /// <summary>
        /// Export Akas to excel
        /// </summary>
        /// <param name="OrderId">A unique identifier for RestrictedPerson</param>
        /// <param name="browseRequest"></param>
        /// <returns></returns>
        // POST api/v1/restrictedperson/{id}/aka/exportexcelxlsx 
        [HttpPost("{orderid}/manifest/exportexcelxlsx")]
        [FwControllerMethod(Id: "KVAhgRQdvHQt", FwControllerActionTypes.ControlBrowse, ParentId: "8uhwXXJ95d3o")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ManifestExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
