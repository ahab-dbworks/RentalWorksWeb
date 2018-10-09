using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.OrderContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class OrderContactController : AppDataController
    {
        public OrderContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderContactLogic>(pageno, pagesize, sort, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordercontact/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderContactLogic>(id, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordercontact 
        [HttpPost]
        public async Task<ActionResult<OrderContactLogic>> PostAsync([FromBody]OrderContactLogic l)
        {
            return await DoPostAsync<OrderContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordercontact/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderContactLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
