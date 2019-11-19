using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.BillingWorksheet
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "2BTZbIXJy4tdI")]
    public class BillingWorksheetController : AppDataController
    {
        public BillingWorksheetController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingWorksheetLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "2BuRM5RuI855b")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet/exportexcelxlsx 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "2DhrshWnhf5qO")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billingworksheet 
        [HttpGet]
        [FwControllerMethod(Id: "2dZcAVnpwsGxX")]
        public async Task<ActionResult<IEnumerable<BillingWorksheetLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BillingWorksheetLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/billingworksheet/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "2ehNCdLgjTrpV")]
        public async Task<ActionResult<BillingWorksheetLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingWorksheetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/billingworksheet 
        [HttpPost]
        [FwControllerMethod(Id: "2eUvhnt0ta38x")]
        public async Task<ActionResult<BillingWorksheetLogic>> PostAsync([FromBody]BillingWorksheetLogic l)
        {
            return await DoPostAsync<BillingWorksheetLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/billingworksheet/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "2GF2QLKIto8hY")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<BillingWorksheetLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
