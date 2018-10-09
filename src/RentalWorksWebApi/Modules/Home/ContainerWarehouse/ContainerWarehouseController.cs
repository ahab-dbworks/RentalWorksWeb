using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ContainerWarehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ContainerWarehouseController : AppDataController
    {
        public ContainerWarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerWarehouseLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containerwarehouse 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContainerWarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerWarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containerwarehouse/A0000001~A0000002
        [HttpGet("{id}")]
        public async Task<ActionResult<ContainerWarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerWarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containerwarehouse 
        [HttpPost]
        public async Task<ActionResult<ContainerWarehouseLogic>> PostAsync([FromBody]ContainerWarehouseLogic l)
        {
            return await DoPostAsync<ContainerWarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containerwarehouse/A0000001~A0000002 
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
