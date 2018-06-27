using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MiscRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class MiscRateController : AppDataController
    {
        public MiscRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(MiscRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/miscrate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscRateLogic>(pageno, pagesize, sort, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscRateLogic>(id, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/miscrate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MiscRateLogic l)
        {
            return await DoPostAsync<MiscRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/miscrate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
} 
