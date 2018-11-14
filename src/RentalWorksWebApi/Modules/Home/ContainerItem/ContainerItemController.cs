using FwStandard.AppManager;
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
    [FwController(Id:"2ZVm2vAYTYJC")]
    public class ContainerItemController : AppDataController
    {
        public ContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iQeXRj8im3k2")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"hq7a4e3uXtez")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem 
        [HttpGet]
        [FwControllerMethod(Id:"Yb3dL1hmjU37")]
        public async Task<ActionResult<IEnumerable<ContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"t1bsMSPUNPNx")]
        public async Task<ActionResult<ContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem 
        [HttpPost]
        [FwControllerMethod(Id:"74qvuZNaDoEB")]
        public async Task<ActionResult<ContainerItemLogic>> PostAsync([FromBody]ContainerItemLogic l)
        {
            return await DoPostAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containeritem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"WhehdFIg4Y8a")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
