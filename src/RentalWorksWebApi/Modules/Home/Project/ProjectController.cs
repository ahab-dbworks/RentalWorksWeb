using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Home.Quote;
using Microsoft.AspNetCore.Http;
using System;

namespace WebApi.Modules.Home.Project
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class ProjectController : AppDataController
    {
        public ProjectController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/browse 
        [HttpPost("browse")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project/A0000001 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project 
        [HttpPost]
        public async Task<ActionResult<ProjectLogic>> PostAsync([FromBody]ProjectLogic l)
        {
            return await DoPostAsync<ProjectLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/project/A0000001 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/createquote/A0000001
        [HttpPost("createquote/{id}")]
        public async Task<ActionResult<QuoteLogic>> CreateQuote([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                ProjectLogic l = new ProjectLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<ProjectLogic>(ids))
                {
                    QuoteLogic lCopy = await l.CreateQuoteAsync();
                    return new OkObjectResult(lCopy);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------      
    }
}
