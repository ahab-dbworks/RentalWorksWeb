using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ItemQc
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"u4UHiW7AOeZ5")]
    public class ItemQcController : AppDataController
    {
        public ItemQcController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ItemQcLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"PXAKe7vpcVKd")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"sLlWnUBW5lq8")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemqc 
        [HttpGet]
        [FwControllerMethod(Id:"gCXQRCvpdlok")]
        public async Task<ActionResult<IEnumerable<ItemQcLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ItemQcLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/itemqc/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4qvndWvRFwXg")]
        public async Task<ActionResult<ItemQcLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ItemQcLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/itemqc 
        [HttpPost]
        [FwControllerMethod(Id:"MsrGQgDVN9s8")]
        public async Task<ActionResult<ItemQcLogic>> PostAsync([FromBody]ItemQcLogic l)
        {
            return await DoPostAsync<ItemQcLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/itemqc/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"dTALdden7B")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await <ItemQcLogic>DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
