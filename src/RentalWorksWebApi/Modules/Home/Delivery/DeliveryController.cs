using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.Delivery
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class DeliveryController : AppDataController
    {
        public DeliveryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DeliveryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DeliveryLogic>(pageno, pagesize, sort, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DeliveryLogic>(id, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery 
        [HttpPost]
        public async Task<ActionResult<DeliveryLogic>> PostAsync([FromBody]DeliveryLogic l)
        {
            return await DoPostAsync<DeliveryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/delivery/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(DeliveryLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
