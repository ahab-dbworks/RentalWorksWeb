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
    public class SpaceController : AppDataController
    {
        public SpaceController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(SpaceLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<SpaceLogic>(pageno, pagesize, sort, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/space/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<SpaceLogic>(id, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/space 
        [HttpPost]
        public async Task<ActionResult<SpaceLogic>> PostAsync([FromBody]SpaceLogic l)
        {
            return await DoPostAsync<SpaceLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/space/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(SpaceLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}