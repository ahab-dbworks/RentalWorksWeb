using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.DiscountItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class DiscountItemController : AppDataController
    {
        public DiscountItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DiscountItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DiscountItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DiscountItemLogic>(pageno, pagesize, sort, typeof(DiscountItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/discountitem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DiscountItemLogic>(id, typeof(DiscountItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/discountitem 
        [HttpPost]
        public async Task<ActionResult<DiscountItemLogic>> PostAsync([FromBody]DiscountItemLogic l)
        {
            return await DoPostAsync<DiscountItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/discountitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DiscountItemLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
