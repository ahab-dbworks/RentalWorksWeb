using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Deal
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class DealController : AppDataController
    {
        public DealController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DealLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal/browse 
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
        // GET api/v1/deal 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DealLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DealLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/deal/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<DealLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DealLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/deal 
        [HttpPost]
        public async Task<ActionResult<DealLogic>> PostAsync([FromBody]DealLogic l)
        {
            return await DoPostAsync<DealLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/deal/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
