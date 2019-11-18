using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.Space
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"DgWXultjwPXkU")]
    public class SpaceController : AppDataController
    {
        public SpaceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"8fuKdCYAyomGi")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"nmnyN0Ncfmo86")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space 
        [HttpGet]
        [FwControllerMethod(Id:"zEML9gVFdqhN9")]
        public async Task<ActionResult<IEnumerable<SpaceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"8znXaLlV6bDRg")]
        public async Task<ActionResult<SpaceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space 
        [HttpPost]
        [FwControllerMethod(Id:"i3TZBoopSGX7u")]
        public async Task<ActionResult<SpaceLogic>> PostAsync([FromBody]SpaceLogic l)
        {
            return await DoPostAsync<SpaceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/space/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"iyDGEgZ6GJMVW")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<SpaceLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
