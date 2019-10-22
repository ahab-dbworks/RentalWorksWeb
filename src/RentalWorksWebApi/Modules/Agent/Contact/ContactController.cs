using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebLibrary;

namespace WebApi.Modules.Agent.Contact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"9ykTwUXTet46")]
    public class ContactController : AppDataController
    {
        public ContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContactLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact/browse
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
        [FwControllerMethod(Id:"nhBg3nFWSLBS")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/contact/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "wfyh5bTW7cCuO")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Crew", RwGlobals.CONTACT_TYPE_CREW_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/contact/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"nhBg3nFWSLBS")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contact
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
        [FwControllerMethod(Id:"tGV9key7gxD6")]
        public async Task<ActionResult<IEnumerable<ContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/contact/A0000001
        [HttpGet("{id}")]
        [Produces(typeof(ContactLogic))]
        [SwaggerResponse(200, Type = typeof(ContactLogic))]
        [FwControllerMethod(Id:"6EMCklyR6Ltw")]
        public async Task<ActionResult<ContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContactLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/contact
        [HttpPost]
        [FwControllerMethod(Id:"3qX4qzUxUYlL")]
        public async Task<ActionResult<ContactLogic>> PostAsync([FromBody]ContactLogic l)
        {
            return await DoPostAsync<ContactLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/contact/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8PPmwft0gjJw")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ContactLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}
