using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PoSettings.PoType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"Gyx3ZcMtuH1fi")]
    public class PoTypeController : AppDataController
    {
        public PoTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PoTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"TidYK8qJ7k9I4")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"MT1G9EiKgacxS")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/potype 
        [HttpGet]
        [FwControllerMethod(Id:"HltbpkfZofilY")]
        public async Task<ActionResult<IEnumerable<PoTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PoTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/potype/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"pPdPDv1bllMz4")]
        public async Task<ActionResult<PoTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PoTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/potype 
        [HttpPost]
        [FwControllerMethod(Id:"IlcCchXHDxfH3")]
        public async Task<ActionResult<PoTypeLogic>> PostAsync([FromBody]PoTypeLogic l)
        {
            return await DoPostAsync<PoTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/potype/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"mjxiwJioKGzPh")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PoTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
