using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.BillingCycle
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class BillingCycleController : AppDataController
    {
        public BillingCycleController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(BillingCycleLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{652BA08D-4136-42FC-84C5-FE89898E3517}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(200, Type = typeof(List<BillingCycleLogic>))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        [Authorize(Policy = "{6960FFCC-430A-4760-88C6-0F07FCFCF851}")]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<BillingCycleLogic>(pageno, pagesize, sort, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/billingcycle/A0000001
        /// <summary></summary>
        /// <remarks></remarks>
        /// <response code="200">Successfully retrieved the record.</response>
        /// <response code="500">An error occured.</response>
        [HttpGet("{id}")]
        [Produces(typeof(BillingCycleLogic))]
        [SwaggerResponse(200, Type = typeof(BillingCycleLogic))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        [Authorize(Policy = "{EC1DF66E-F686-4BF4-A61C-19CAF8FA3EE7}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<BillingCycleLogic>(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/billingcycle
        [HttpPost]
        [Authorize(Policy = "{D1D60908-B2F3-46E3-9ED3-DD9312DA4323}")]
        public async Task<IActionResult> PostAsync([FromBody]BillingCycleLogic l)
        {
            return await DoPostAsync<BillingCycleLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/billingcycle/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{DECE507D-7730-4759-959C-8BB54CDBAFC4}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(BillingCycleLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}