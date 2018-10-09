using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationLayerForm
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PresentationLayerFormController : AppDataController
    {
        public PresentationLayerFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresentationLayerFormLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<PresentationLayerFormLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform 
        [HttpPost]
        public async Task<ActionResult<PresentationLayerFormLogic>> PostAsync([FromBody]PresentationLayerFormLogic l)
        {
            return await DoPostAsync<PresentationLayerFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayerform/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}