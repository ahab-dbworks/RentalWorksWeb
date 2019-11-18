using FwStandard.AppManager;
using FwStandard.SqlServer;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ContractItemSummary
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"a8I3UCKA3LN3")]
    public class ContractItemSummaryController : AppDataController
    {
        public ContractItemSummaryController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContractItemSummaryLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contractitemsummary/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"7CZjYab2Qpl4")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contractitemsummary/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"qUBgRCLqkvkn")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
    }
}
