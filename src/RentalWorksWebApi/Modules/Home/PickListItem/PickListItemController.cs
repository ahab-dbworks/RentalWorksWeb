using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PickListItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PickListItemController : AppDataController
    {
        public PickListItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem/browse 
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
        // GET api/v1/picklistitem 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PickListItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklistitem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PickListItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklistitem 
        [HttpPost]
        public async Task<ActionResult<PickListItemLogic>> PostAsync([FromBody]PickListItemLogic l)
        {
            return await DoPostAsync<PickListItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklistitem/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}