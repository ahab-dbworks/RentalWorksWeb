using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PaymentSettings.PaymentType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"d8RdKxFfho4z")]
    public class PaymentTypeController : AppDataController
    {
        public PaymentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"GLIvBMjLCcKM")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"LCO64veg3hw4")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype
        [HttpGet]
        [FwControllerMethod(Id:"jwuaS4gCHxVR")]
        public async Task<ActionResult<IEnumerable<PaymentTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"5a0oHVzRlCEd")]
        public async Task<ActionResult<PaymentTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype
        [HttpPost]
        [FwControllerMethod(Id:"gubRbuYcJuvz")]
        public async Task<ActionResult<PaymentTypeLogic>> PostAsync([FromBody]PaymentTypeLogic l)
        {
            return await DoPostAsync<PaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymenttype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"W4dBHcERzFAK")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PaymentTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
