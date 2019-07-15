using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerActivityOverride
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"HWjX0WDoiG79H")]
    public class PresentationLayerActivityOverrideController : AppDataController
    {
        public PresentationLayerActivityOverrideController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerActivityOverrideLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"QQ4urqItSSUZr")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"8Yv1SCYVPCvvu")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride 
        [HttpGet]
        [FwControllerMethod(Id:"sTW80cbSWsG2B")]
        public async Task<ActionResult<IEnumerable<PresentationLayerActivityOverrideLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivityoverride/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"unn07ckPnbffx")]
        public async Task<ActionResult<PresentationLayerActivityOverrideLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityOverrideLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivityoverride 
        [HttpPost]
        [FwControllerMethod(Id:"NdQdtgeLlIeuU")]
        public async Task<ActionResult<PresentationLayerActivityOverrideLogic>> PostAsync([FromBody]PresentationLayerActivityOverrideLogic l)
        {
            return await DoPostAsync<PresentationLayerActivityOverrideLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivityoverride/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"kxiO2Eoj2cdmk")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PresentationLayerActivityOverrideLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
