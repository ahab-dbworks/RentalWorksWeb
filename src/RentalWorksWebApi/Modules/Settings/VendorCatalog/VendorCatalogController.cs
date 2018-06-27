using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Settings.VendorCatalog
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class VendorCatalogController : AppDataController
    {
        public VendorCatalogController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VendorCatalogLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog/browse
        /// <summary>
        /// Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieved records.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpPost("browse")]
        [Authorize(Policy = "{33F721F5-0D91-464C-AFA7-FA46622CE3C0}")]
        //[ApiExplorerSettings(IgnoreApi=true)]
        public async Task<IActionResult> Browse([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(VendorCatalogLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog
        /// <summary>
        /// REST: Retrieves a list of records
        /// </summary>
        /// <remarks>Use the pageno and pagesize query string parameters to limit the result set and improve performance.</remarks>
        /// <response code="200">Successfully retrieved records.</response>
        /// <response code="401">Unauthorized - A valid JWT Bearer Token must be provided in the header.</response>
        /// <response code="403">Forbidden - User is authenticated but does not have permission to access this resource.</response>
        /// <response code="500">Error in the webapi or database.</response>
        [HttpGet]
        [Authorize(Policy = "{D1CECF78-CC21-4B3C-8F16-11D31B996032}")]
        [Produces(typeof(List<VendorCatalogLogic>))]
        [SwaggerResponse(200, Type = typeof(List<VendorCatalogLogic>))]
        [SwaggerResponse(401, Type = typeof(string))]
        [SwaggerResponse(403, Type = typeof(string))]
        [SwaggerResponse(500, Type = typeof(FwApiException))]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VendorCatalogLogic>(pageno, pagesize, sort, typeof(VendorCatalogLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vendorcatalog/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{FF697F23-9150-4252-8C58-A0063419B88E}")]
        [Produces(typeof(VendorCatalogLogic))]
        [SwaggerResponse(200, Type = typeof(VendorCatalogLogic))]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VendorCatalogLogic>(id, typeof(VendorCatalogLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vendorcatalog
        [HttpPost]
        [Authorize(Policy = "{0178C657-4473-4889-945A-A2F88B3D31C0}")]
        public async Task<IActionResult> PostAsync([FromBody]VendorCatalogLogic l)
        {
            return await DoPostAsync<VendorCatalogLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vendorcatalog/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{CDA85B7B-F766-410C-9B8E-D0DEFA313341}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(VendorCatalogLogic));
        }
        //------------------------------------------------------------------------------------
    }
}