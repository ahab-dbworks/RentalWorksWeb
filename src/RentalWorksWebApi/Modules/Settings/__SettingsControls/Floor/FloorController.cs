using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Floor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"LrybQVClgY6f")]
    public class FloorController : AppDataController
    {
        public FloorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FloorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"I5XCMOmSoDZ5")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"yK5WFt4LwLJi")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor 
        [HttpGet]
        [FwControllerMethod(Id:"eBU90uF94BeU")]
        public async Task<ActionResult<IEnumerable<FloorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FloorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"tkeJBbqpX9ms")]
        public async Task<ActionResult<FloorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FloorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor 
        [HttpPost]
        [FwControllerMethod(Id:"Oh83FN8lsl0D")]
        public async Task<ActionResult<FloorLogic>> PostAsync([FromBody]FloorLogic l)
        {
            return await DoPostAsync<FloorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/floor/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"WlKt8i6QIrfs")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FloorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
