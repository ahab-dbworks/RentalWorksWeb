using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.OrderSettings.OrderSetNo
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"OoepsrkqPYRP")]
    public class OrderSetNoController : AppDataController
    {
        public OrderSetNoController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(OrderSetNoLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"S21ps5UYCTgd")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"2nWIZHuWwxdS")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno 
        [HttpGet]
        [FwControllerMethod(Id:"PVe7DYvQXsjh")]
        public async Task<ActionResult<IEnumerable<OrderSetNoLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<OrderSetNoLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/ordersetno/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3cS7eEZ92XOG")]
        public async Task<ActionResult<OrderSetNoLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<OrderSetNoLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/ordersetno 
        [HttpPost]
        [FwControllerMethod(Id:"LlN0BR5vxZg3")]
        public async Task<ActionResult<OrderSetNoLogic>> PostAsync([FromBody]OrderSetNoLogic l)
        {
            return await DoPostAsync<OrderSetNoLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/ordersetno/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"booSUfWreDwC")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<OrderSetNoLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
