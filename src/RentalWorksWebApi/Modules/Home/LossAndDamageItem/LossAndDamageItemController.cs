using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.LossAndDamageItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"ATHmxMHmRo9u")]
    public class LossAndDamageItemController : AppDataController
    {
        public LossAndDamageItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(LossAndDamageItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/lossanddamageitem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"dDavaElCOQgm")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/lossanddamageitem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"ZpMO62JkDN92")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
