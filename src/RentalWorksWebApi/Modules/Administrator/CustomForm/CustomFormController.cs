using FwStandard.AppManager;
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
    [FwController(Id:"xfpeg2SVZW")]
    public class CustomFormController : AppDataController
    {
        public CustomFormController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(CustomFormLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"g3npBLUNZHM")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"9f1VVqKF0Dc")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customform 
        [HttpGet]
        [FwControllerMethod(Id:"uVPdJ9BXtkA")]
        public async Task<ActionResult<IEnumerable<CustomFormLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<CustomFormLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/customform/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"4YKuNhms4Xi")]
        public async Task<ActionResult<CustomFormLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<CustomFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/customform 
        [HttpPost]
        [FwControllerMethod(Id:"4jN6aEnmSxM")]
        public async Task<ActionResult<CustomFormLogic>> PostAsync([FromBody]CustomFormLogic l)
        {
            return await DoPostAsync<CustomFormLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/customform/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"lC1HeziHXYU")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<CustomFormLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
