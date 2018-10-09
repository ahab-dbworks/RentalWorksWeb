using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PaymentType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PaymentTypeController : AppDataController
    {
        public PaymentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PaymentTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype/browse
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PaymentTypeLogic>(pageno, pagesize, sort, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/paymenttype/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PaymentTypeLogic>(id, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/paymenttype
        [HttpPost]
        public async Task<ActionResult<PaymentTypeLogic>> PostAsync([FromBody]PaymentTypeLogic l)
        {
            return await DoPostAsync<PaymentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/paymenttype/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PaymentTypeLogic));
        }
        //------------------------------------------------------------------------------------
    }
}