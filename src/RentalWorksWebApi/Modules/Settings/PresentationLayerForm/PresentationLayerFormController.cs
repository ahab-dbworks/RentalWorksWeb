using FwStandard.AppManager;
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
    [FwController(Id:"FcJ0Ld64KSUqv")]
    public class PresentationLayerFormController : AppDataController
    {
        public PresentationLayerFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"boplIDVdCe7Ku")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"5BV5RfNBBSl7G")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform 
        [HttpGet]
        [FwControllerMethod(Id:"m95DmuszGpzLc")]
        public async Task<ActionResult<IEnumerable<PresentationLayerFormLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayerform/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"f47QUohEZdqeb")]
        public async Task<ActionResult<PresentationLayerFormLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayerform 
        [HttpPost]
        [FwControllerMethod(Id:"rngFOJdSSgkrM")]
        public async Task<ActionResult<PresentationLayerFormLogic>> PostAsync([FromBody]PresentationLayerFormLogic l)
        {
            return await DoPostAsync<PresentationLayerFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayerform/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"k3RTSk2ADzNRD")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PresentationLayerFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
