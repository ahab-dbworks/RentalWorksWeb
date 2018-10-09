using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.PickList
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class PickListController : AppDataController
    {
        public PickListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PickListLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PickListLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PickListLogic>(pageno, pagesize, sort, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/picklist/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PickListLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PickListLogic>(id, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/picklist 
        [HttpPost]
        public async Task<ActionResult<PickListLogic>> PostAsync([FromBody]PickListLogic l)
        {
            return await DoPostAsync<PickListLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/picklist/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PickListLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}