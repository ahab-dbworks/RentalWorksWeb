using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.BillingCycleSettings.BillingCycle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"FQfXEIcN9q3")]
    public class BillingCycleController : AppDataController
    {
        public BillingCycleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingCycleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ORQSLFHiEvE")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"sl9F38QWINr")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(200, Type = typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        [FwControllerMethod(Id:"sXXyXtvhW8p")]
        public async Task<ActionResult<IEnumerable<BillingCycleLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BillingCycleLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle/A0000001
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieved the record.</response>
        /// <response code="500">An error occured.</response>
        [HttpGet("{id}")]
        [Produces(typeof(BillingCycleLogic))]
        [SwaggerResponse(200, Type = typeof(BillingCycleLogic))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        [FwControllerMethod(Id:"Y9nqSbDoh07")]
        public async Task<ActionResult<BillingCycleLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingCycleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle
        [HttpPost]
        [FwControllerMethod(Id:"u0NKWxoLFys")]
        public async Task<ActionResult<BillingCycleLogic>> PostAsync([FromBody]BillingCycleLogic l)
        {
            return await DoPostAsync<BillingCycleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycle/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Vfj8RsMJma2")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BillingCycleLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
