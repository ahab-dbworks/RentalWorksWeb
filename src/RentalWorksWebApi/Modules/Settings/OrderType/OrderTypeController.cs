using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace WebApi.Modules.Settings.OrderType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class OrderTypeController : AppDataController
    {
        public OrderTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderTypeLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderTypeLogic>(pageno, pagesize, sort, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordertype/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderTypeLogic>(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordertype 
        [HttpPost]
        public async Task<ActionResult<OrderTypeLogic>> PostAsync([FromBody]OrderTypeLogic l)
        {
            return await DoPostAsync<OrderTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordertype/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(OrderTypeLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}