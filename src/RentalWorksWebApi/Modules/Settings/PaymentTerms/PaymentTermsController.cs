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
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTermsLogic>(pageno, pagesize, sort, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymentterms/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTermsLogic>(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymentterms
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PaymentTermsLogic l)
        {
            return await DoPostAsync<PaymentTermsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymentterms/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PaymentTermsLogic));
        }
        //------------------------------------------------------------------------------------
    }
}