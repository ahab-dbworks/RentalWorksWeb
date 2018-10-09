using FwStandard.SqlServer;
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
    public class PaymentTermsController : AppDataController
    {
        public PaymentTermsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentTermsLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTermsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTermsLogic>(pageno, pagesize, sort, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTermsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTermsLogic>(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms
        [HttpPost]
        public async Task<ActionResult<PaymentTermsLogic>> PostAsync([FromBody]PaymentTermsLogic l)
        {
            return await DoPostAsync<PaymentTermsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymentterms/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
    }
}