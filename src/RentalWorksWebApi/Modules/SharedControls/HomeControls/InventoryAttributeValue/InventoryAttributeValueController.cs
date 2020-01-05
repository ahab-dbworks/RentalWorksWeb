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
namespace WebApi.Modules.HomeControls.InventoryAttributeValue
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"CntxgVXDQtQ7")]
    public class InventoryAttributeValueController : AppDataController
    {
        public InventoryAttributeValueController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventoryAttributeValueLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"b5UF2WxLbYB8", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"5kP4sYPGaQ7w", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue 
        [HttpGet]
        [FwControllerMethod(Id:"swtHPwwFbRLy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<InventoryAttributeValueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"wRDOzPLvwbMD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<InventoryAttributeValueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue 
        [HttpPost]
        [FwControllerMethod(Id:"algeSS932VS0", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventoryAttributeValueLogic>> NewAsync([FromBody]InventoryAttributeValueLogic l)
        {
            return await DoNewAsync<InventoryAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventoryattributevalue/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "wbGWXLXFlHOd4", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<InventoryAttributeValueLogic>> EditAsync([FromRoute] string id, [FromBody]InventoryAttributeValueLogic l)
        {
            return await DoEditAsync<InventoryAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryattributevalue/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Ra1n1jcOc9zk", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue/validateattribute/browse
        [HttpPost("validateattribute/browse")]
        [FwControllerMethod(Id: "M8cASpupwu40", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue/validateattributevalue/browse
        [HttpPost("validateattributevalue/browse")]
        [FwControllerMethod(Id: "e2ph9lBdr8vU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAttributeValueBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<AttributeValueLogic>(browseRequest);
        }
    }
}
