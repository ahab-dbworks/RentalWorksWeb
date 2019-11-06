using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.InventoryAttributeValue
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
        [FwControllerMethod(Id:"b5UF2WxLbYB8")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"5kP4sYPGaQ7w")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue 
        [HttpGet]
        [FwControllerMethod(Id:"swtHPwwFbRLy")]
        public async Task<ActionResult<IEnumerable<InventoryAttributeValueLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/inventoryattributevalue/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"wRDOzPLvwbMD")]
        public async Task<ActionResult<InventoryAttributeValueLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<InventoryAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventoryattributevalue 
        [HttpPost]
        [FwControllerMethod(Id:"algeSS932VS0")]
        public async Task<ActionResult<InventoryAttributeValueLogic>> PostAsync([FromBody]InventoryAttributeValueLogic l)
        {
            return await DoPostAsync<InventoryAttributeValueLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/inventoryattributevalue/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"Ra1n1jcOc9zk")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<InventoryAttributeValueLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
