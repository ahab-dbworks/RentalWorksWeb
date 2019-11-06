using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DocumentBarCodeSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id: "iSSvVLPqOGXnD")]
    public class DocumentBarCodeSettingsController : AppDataController
    {
        public DocumentBarCodeSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DocumentBarCodeSettingsLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/documentbarcodesettings/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "HmH02lzV7F7rI")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/documentbarcodesettings/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "7tfjeobUPbZtp")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/documentbarcodesettings 
        [HttpGet]
        [FwControllerMethod(Id: "tfV5rv6FWdvRL")]
        public async Task<ActionResult<IEnumerable<DocumentBarCodeSettingsLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<DocumentBarCodeSettingsLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/documentbarcodesettings/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "LcXNRhaljL3zH")]
        public async Task<ActionResult<DocumentBarCodeSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<DocumentBarCodeSettingsLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/documentbarcodesettings 
        [HttpPost]
        [FwControllerMethod(Id: "y01S406DRvr6U")]
        public async Task<ActionResult<DocumentBarCodeSettingsLogic>> PostAsync([FromBody]DocumentBarCodeSettingsLogic l)
        {
            return await DoPostAsync<DocumentBarCodeSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
