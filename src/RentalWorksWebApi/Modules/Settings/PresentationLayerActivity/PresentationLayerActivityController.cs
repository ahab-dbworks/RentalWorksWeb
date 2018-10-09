using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PresentationLayerActivityController : AppDataController
    {
        public PresentationLayerActivityController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerActivityLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentationLayerActivityLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(pageno, pagesize, sort, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayeractivity/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PresentationLayerActivityLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerActivityLogic>(id, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayeractivity 
        [HttpPost]
        public async Task<ActionResult<PresentationLayerActivityLogic>> PostAsync([FromBody]PresentationLayerActivityLogic l)
        {
            return await DoPostAsync<PresentationLayerActivityLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayeractivity/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PresentationLayerActivityLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}