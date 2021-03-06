using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Settings.InventorySettings.Attribute;
using WebApi.Modules.Settings.AttributeValue;
namespace WebApi.Modules.HomeControls.ItemAttributeValue
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
        [FwControllerMethod(Id:"gtfsNtPMKteH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ThqZWUbchzDX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue 
        [HttpGet]
        [FwControllerMethod(Id:"X6zAgQxhG0RZ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ItemAttributeValueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemattributevalue/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"h9VMJrFaxxfE", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ItemAttributeValueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue 
        [HttpPost]
        [FwControllerMethod(Id:"KM22ukR1dGhR", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ItemAttributeValueLogic>> NewAsync([FromBody]ItemAttributeValueLogic l)
        {
            return await DoNewAsync<ItemAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/itemattributevalue/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "sLP438DDYgUhz", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ItemAttributeValueLogic>> EditAsync([FromRoute] string id, [FromBody]ItemAttributeValueLogic l)
        {
            return await DoEditAsync<ItemAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/itemattributevalue/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"aTs8XwiiT3SJ", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ItemAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue/validateattribute/browse
        [HttpPost("validateattribute/browse")]
        [FwControllerMethod(Id: "f5937sVY7A4E", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemattributevalue/validateattributevalue/browse
        [HttpPost("validateattributevalue/browse")]
        [FwControllerMethod(Id: "0dcM4d3M0tlJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeValueBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeValueLogic>(browseRequest);
        }
    }
}
