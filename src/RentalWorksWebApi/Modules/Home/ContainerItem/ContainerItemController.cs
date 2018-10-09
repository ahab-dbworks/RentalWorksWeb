using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ContainerItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ContainerItemController : AppDataController
    {
        public ContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerItemLogic>(pageno, pagesize, sort, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerItemLogic>(id, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem 
        [HttpPost]
        public async Task<ActionResult<ContainerItemLogic>> PostAsync([FromBody]ContainerItemLogic l)
        {
            return await DoPostAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containeritem/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ContainerItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
    }
}