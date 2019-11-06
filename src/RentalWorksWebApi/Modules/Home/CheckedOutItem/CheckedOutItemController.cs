using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.CheckedOutItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"HXSEu4U0vSir")]
    public class CheckedOutItemController : AppDataController
    {
        public CheckedOutItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CheckedOutItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkedoutitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"6idKHFiAfXUO")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/checkedoutitem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"9BL5h5mVJimA")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
