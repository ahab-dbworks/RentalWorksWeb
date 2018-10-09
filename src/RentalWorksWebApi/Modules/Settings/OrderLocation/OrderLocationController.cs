using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderLocationController : AppDataController
    {
        public OrderLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderLocationLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation/browse 
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
        // GET api/v1/orderlocation 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLocationLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderLocationLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/orderlocation/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLocationLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderLocationLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/orderlocation 
        [HttpPost]
        public async Task<ActionResult<OrderLocationLogic>> PostAsync([FromBody]OrderLocationLogic l)
        {
            return await DoPostAsync<OrderLocationLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/orderlocation/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}