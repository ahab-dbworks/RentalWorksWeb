using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Home.Contact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ContactController : AppDataController
    {
        public ContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/browse
        /// <summary>
        /// Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpPost("browse")]
        [Produces(typeof(FwJsonDataTable))]
        [SwaggerResponse(200, Type = typeof(FwJsonDataTable))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(ContactLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus
        /// <summary>
        /// REST: Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieves records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Produces(typeof(List<ContactLogic>))]
        [SwaggerResponse(200, Type = typeof(List<ContactLogic>))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContactLogic>(pageno, pagesize, sort, typeof(ContactLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/customerstatus/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(ContactLogic))]
        [SwaggerResponse(200, Type = typeof(ContactLogic))]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContactLogic>(id, typeof(ContactLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]ContactLogic l)
        {
            return await DoPostAsync<ContactLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/customerstatus/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(ContactLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/customerstatus/validateduplicate
        [HttpPost("validateduplicate")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}