using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.CustomField
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class CustomFieldController : AppDataController
    {
        public CustomFieldController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFieldLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customfield/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CustomFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customfield 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomFieldLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFieldLogic>(pageno, pagesize, sort, typeof(CustomFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customfield/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomFieldLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFieldLogic>(id, typeof(CustomFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customfield 
        [HttpPost]
        public async Task<ActionResult<CustomFieldLogic>> PostAsync([FromBody]CustomFieldLogic l)
        {
            return await DoPostAsync<CustomFieldLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customfield/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(CustomFieldLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}