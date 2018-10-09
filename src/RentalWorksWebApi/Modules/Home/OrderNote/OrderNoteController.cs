using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderNoteController : AppDataController
    {
        public OrderNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote/browse 
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
        // GET api/v1/ordernote 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderNoteLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordernote/A0000001
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderNoteLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordernote 
        [HttpPost]
        public async Task<ActionResult<OrderNoteLogic>> PostAsync([FromBody]OrderNoteLogic l)
        {
            return await DoPostAsync<OrderNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordernote/A0000001
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}