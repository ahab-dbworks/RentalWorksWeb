using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ItemAttributeValue
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"buplkDkxM1hC")]
    public class ItemAttributeValueController : AppDataController
    {
        public ItemAttributeValueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemAttributeValueLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"gtfsNtPMKteH")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ThqZWUbchzDX")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue 
        [HttpGet]
        [FwControllerMethod(Id:"X6zAgQxhG0RZ")]
        public async Task<ActionResult<IEnumerable<ItemAttributeValueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"h9VMJrFaxxfE")]
        public async Task<ActionResult<ItemAttributeValueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue 
        [HttpPost]
        [FwControllerMethod(Id:"KM22ukR1dGhR")]
        public async Task<ActionResult<ItemAttributeValueLogic>> PostAsync([FromBody]ItemAttributeValueLogic l)
        {
            return await DoPostAsync<ItemAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/itemattributevalue/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"aTs8XwiiT3SJ")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ItemAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
