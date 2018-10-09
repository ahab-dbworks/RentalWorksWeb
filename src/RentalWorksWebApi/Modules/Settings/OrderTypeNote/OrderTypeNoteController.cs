using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderTypeNote
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeNoteController : AppDataController
    {
        public OrderTypeNoteController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeNoteLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTypeNoteLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(pageno, pagesize, sort, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertypenote/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTypeNoteLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeNoteLogic>(id, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertypenote 
        [HttpPost]
        public async Task<ActionResult<OrderTypeNoteLogic>> PostAsync([FromBody]OrderTypeNoteLogic l)
        {
            return await DoPostAsync<OrderTypeNoteLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertypenote/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeNoteLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}