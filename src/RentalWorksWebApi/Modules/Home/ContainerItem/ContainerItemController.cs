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
        // POST api/v1/item/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerItemLogic>(pageno, pagesize, sort, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerItemLogic>(id, typeof(ContainerItemLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContainerItemLogic l)
        {
            return await DoPostAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containeritem/A0000001 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id, typeof(ContainerItemLogic));
        //}
        ////------------------------------------------------------------------------------------ 
    }
}