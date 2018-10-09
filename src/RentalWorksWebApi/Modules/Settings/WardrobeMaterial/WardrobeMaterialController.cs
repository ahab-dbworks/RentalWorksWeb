using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.WardrobeMaterial
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class WardrobeMaterialController : AppDataController
    {
        public WardrobeMaterialController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WardrobeMaterialLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/wardrobematerial/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/wardrobematerial 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WardrobeMaterialLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeMaterialLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/wardrobematerial/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<WardrobeMaterialLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeMaterialLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/wardrobematerial 
        [HttpPost]
        public async Task<ActionResult<WardrobeMaterialLogic>> PostAsync([FromBody]WardrobeMaterialLogic l)
        {
            return await DoPostAsync<WardrobeMaterialLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/wardrobematerial/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}