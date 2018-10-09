using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ItemQc
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ItemQcController : AppDataController
    {
        public ItemQcController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemQcLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc/browse 
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
        // GET api/v1/itemqc 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemQcLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemQcLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemqc/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemQcLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemQcLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc 
        [HttpPost]
        public async Task<ActionResult<ItemQcLogic>> PostAsync([FromBody]ItemQcLogic l)
        {
            return await DoPostAsync<ItemQcLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/itemqc/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}