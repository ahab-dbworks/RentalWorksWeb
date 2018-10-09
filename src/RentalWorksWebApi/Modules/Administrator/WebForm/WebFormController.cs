using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.WebForm
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class WebFormController : AppDataController
    {
        public WebFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WebFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webform/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webform/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webform 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebFormLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WebFormLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/webform/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<WebFormLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WebFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/webform 
        [HttpPost]
        public async Task<ActionResult<WebFormLogic>> PostAsync([FromBody]WebFormLogic l)
        {
            return await DoPostAsync<WebFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/webform/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
