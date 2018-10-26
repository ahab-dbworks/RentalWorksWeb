using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Administrator.CustomForm
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    public class CustomFormController : AppDataController
    {
        public CustomFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customform 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomFormLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFormLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customform/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomFormLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform 
        [HttpPost]
        public async Task<ActionResult<CustomFormLogic>> PostAsync([FromBody]CustomFormLogic l)
        {
            return await DoPostAsync<CustomFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customform/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
