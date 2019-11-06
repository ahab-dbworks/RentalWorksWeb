using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Agent.Quote;

namespace WebApi.Modules.Agent.Project
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"k7bYJRoHkf9Jr")]
    public class ProjectController : AppDataController
    {
        public ProjectController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"4k50xVthJKNTW")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"74Hn2Rn76j6hj")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project 
        [HttpGet]
        [FwControllerMethod(Id:"zNZ938we8yIyS")]
        public async Task<ActionResult<IEnumerable<ProjectLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"iOnfqHPsaizYm")]
        public async Task<ActionResult<ProjectLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project 
        [HttpPost]
        [FwControllerMethod(Id:"fFUzpeoiwQKIa")]
        public async Task<ActionResult<ProjectLogic>> PostAsync([FromBody]ProjectLogic l)
        {
            return await DoPostAsync<ProjectLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/project/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"uvLNiRZFF5gdy")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/createquote
        [HttpPost("createquote")]
        [FwControllerMethod(Id:"X5qRQcu9TBn8Z")]
        public async Task<ActionResult<CreateQuoteFromProjectResponse>> CreateQuote([FromBody]CreateQuoteFromProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProjectLogic l = new ProjectLogic();
                l.SetDependencies(AppConfig, UserSession);
                l.ProjectId = request.ProjectId;
                if (await l.LoadAsync<ProjectLogic>())
                {
                    CreateQuoteFromProjectResponse response = ProjectFunc.CreateQuoteFromProject(AppConfig, UserSession, request).Result;
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------      
    }
}
