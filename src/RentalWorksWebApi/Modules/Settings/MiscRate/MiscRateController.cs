using FwStandard.SqlServer;
using System.Collections.Generic;
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
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MiscRateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<MiscRateLogic>(pageno, pagesize, sort, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/miscrate/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<MiscRateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<MiscRateLogic>(id, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/miscrate 
        [HttpPost]
        public async Task<ActionResult<MiscRateLogic>> PostAsync([FromBody]MiscRateLogic l)
        {
            return await DoPostAsync<MiscRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/miscrate/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(MiscRateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
} 
