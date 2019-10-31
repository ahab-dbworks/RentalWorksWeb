using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebLibrary;

namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"QiLcE27ZUg0sE")]
    public class PresentationLayerActivityController : AppDataController
    {
        public PresentationLayerActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "ZUHUUZRtRmN")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("User Defined", RwGlobals.PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"JKJKOGENMZz7r")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"XrQiZ0yrmh02d")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity 
        [HttpGet]
        [FwControllerMethod(Id:"f1sb2nrRGroY5")]
        public async Task<ActionResult<IEnumerable<PresentationLayerActivityLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"kHPfizupSWsse")]
        public async Task<ActionResult<PresentationLayerActivityLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity 
        [HttpPost]
        [FwControllerMethod(Id:"bMwrxDvhOjxmd")]
        public async Task<ActionResult<PresentationLayerActivityLogic>> PostAsync([FromBody]PresentationLayerActivityLogic l)
        {
            return await DoPostAsync<PresentationLayerActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivity/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"QW3iMQBuMJNfh")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PresentationLayerActivityLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
