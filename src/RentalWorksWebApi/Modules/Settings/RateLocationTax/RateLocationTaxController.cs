using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.RateLocationTax
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Bm6TN9A4IRIuT")]
    public class RateLocationTaxController : AppDataController
    {
        public RateLocationTaxController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RateLocationTaxLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"aVeCL8xWSJD0e")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Ttqpe6JUemjIp")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax 
        [HttpGet]
        [FwControllerMethod(Id:"R46ksUprfrt9l")]
        public async Task<ActionResult<IEnumerable<RateLocationTaxLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RateLocationTaxLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ratelocationtax/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"eT1x6IGk9vPS3")]
        public async Task<ActionResult<RateLocationTaxLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RateLocationTaxLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ratelocationtax 
        [HttpPost]
        [FwControllerMethod(Id:"bueQpYL0H3yj4")]
        public async Task<ActionResult<RateLocationTaxLogic>> PostAsync([FromBody]RateLocationTaxLogic l)
        {
            return await DoPostAsync<RateLocationTaxLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ratelocationtax/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"1mMPF6XDmwjr1")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RateLocationTaxLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
