using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.GeneralItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class GeneralItemController : AppDataController
    {
        public GeneralItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(GeneralItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/generalitem/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/generalitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/generalitem 
        //[HttpGet]
        //public aync <IEnumerable<GeneralItemLogic>>Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<GeneralItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/generalitem/A0000001 
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GeneralItemLogic>> GetOneAsync([FromRoute]string id)
        //{
        //    return await DoGetAsync<GeneralItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
