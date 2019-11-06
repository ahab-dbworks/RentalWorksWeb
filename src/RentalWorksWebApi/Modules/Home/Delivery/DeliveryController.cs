using FwStandard.AppManager;
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
    [FwController(Id:"WY2jiBt4BUhw")]
    public class DeliveryController : AppDataController
    {
        public DeliveryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DeliveryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"HxGkco9pTe2T")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"sDOn6A87bzbQ")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery 
        [HttpGet]
        [FwControllerMethod(Id:"vKppc8brTSBZ")]
        public async Task<ActionResult<IEnumerable<DeliveryLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DeliveryLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/delivery/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"zTrR7Y5WVdQC")]
        public async Task<ActionResult<DeliveryLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DeliveryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/delivery 
        [HttpPost]
        [FwControllerMethod(Id:"8V2uu3qx6qwv")]
        public async Task<ActionResult<DeliveryLogic>> PostAsync([FromBody]DeliveryLogic l)
        {
            return await DoPostAsync<DeliveryLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/delivery/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"zuwWc7irvQxh")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<DeliveryLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
