using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PaymentTerms
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"p5RqSdENdPMa")]
    public class PaymentTermsController : AppDataController
    {
        public PaymentTermsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentTermsLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"AM6PansBnljo")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"dQuJzJR6gQkh")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms
        [HttpGet]
        [FwControllerMethod(Id:"vknFa8QrPuSC")]
        public async Task<ActionResult<IEnumerable<PaymentTermsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTermsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"0kkgYaWCcKmd")]
        public async Task<ActionResult<PaymentTermsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTermsLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms
        [HttpPost]
        [FwControllerMethod(Id:"ARB5oWOjjLmB")]
        public async Task<ActionResult<PaymentTermsLogic>> PostAsync([FromBody]PaymentTermsLogic l)
        {
            return await DoPostAsync<PaymentTermsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymentterms/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"0WkQuBudUoQ7")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PaymentTermsLogic>(id);
        }
        //------------------------------------------------------------------------------------
    }
}
