using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PresentationLayerController : AppDataController
    {
        public PresentationLayerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayer/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PresentationLayerLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayer 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentationLayerLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerLogic>(pageno, pagesize, sort, typeof(PresentationLayerLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayer/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PresentationLayerLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerLogic>(id, typeof(PresentationLayerLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayer 
        [HttpPost]
        public async Task<ActionResult<PresentationLayerLogic>> PostAsync([FromBody]PresentationLayerLogic l)
        {
            return await DoPostAsync<PresentationLayerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayer/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PresentationLayerLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}