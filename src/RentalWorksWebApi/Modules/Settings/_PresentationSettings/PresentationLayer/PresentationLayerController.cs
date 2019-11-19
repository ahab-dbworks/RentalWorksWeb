using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Settings.PresentationSettings.PresentationLayer
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"0v54dFE9Zhun8")]
    public class PresentationLayerController : AppDataController
    {
        public PresentationLayerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PresentationLayerLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayer/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"XFSWBHZMLvK7I", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Yx9RVf2qzpyVy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayer 
        [HttpGet]
        [FwControllerMethod(Id:"045SaetGaOLor", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<PresentationLayerLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PresentationLayerLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/presentationlayer/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"sXJKrdKGwAqIQ", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<PresentationLayerLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PresentationLayerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/presentationlayer 
        [HttpPost]
        [FwControllerMethod(Id:"1jJxBZFURTBdL", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<PresentationLayerLogic>> NewAsync([FromBody]PresentationLayerLogic l)
        {
            return await DoNewAsync<PresentationLayerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/presentationlayer/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "DjQsDmgvqDPas", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<PresentationLayerLogic>> EditAsync([FromRoute] string id, [FromBody]PresentationLayerLogic l)
        {
            return await DoEditAsync<PresentationLayerLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/presentationlayer/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"FjecJyzrBwk1i", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<PresentationLayerLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
