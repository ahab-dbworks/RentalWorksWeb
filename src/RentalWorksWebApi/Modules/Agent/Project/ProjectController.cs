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
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Contact;

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
        [FwControllerMethod(Id:"4k50xVthJKNTW", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"74Hn2Rn76j6hj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project 
        [HttpGet]
        [FwControllerMethod(Id:"zNZ938we8yIyS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<ProjectLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/project/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"iOnfqHPsaizYm", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<ProjectLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project 
        [HttpPost]
        [FwControllerMethod(Id:"fFUzpeoiwQKIa", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ProjectLogic>> NewAsync([FromBody]ProjectLogic l)
        {
            return await DoNewAsync<ProjectLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/project/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "3amC6YjsCScYW", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ProjectLogic>> EditAsync([FromRoute] string id, [FromBody]ProjectLogic l)
        {
            return await DoEditAsync<ProjectLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/project/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"uvLNiRZFF5gdy", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/project/createquote
        [HttpPost("createquote")]
        [FwControllerMethod(Id:"X5qRQcu9TBn8Z", ActionType: FwControllerActionTypes.Option, Caption:"Create Quote")]
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
        // POST api/v1/project/validatedeal/browse
        [HttpPost("validatedeal/browse")]
        [FwControllerMethod(Id: "uymPkefBnT1F", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/project/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "WEvNuu5Ucyfn", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/project/validateagent/browse
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "y4ekIIQPdLv7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/project/validateprojectmanager/browse
        [HttpPost("validateprojectmanager/browse")]
        [FwControllerMethod(Id: "YsdazDF3nCOw", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateProjectManagerBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------      
        // POST api/v1/project/validatesalesrepresentative/browse
        [HttpPost("validatesalesrepresentative/browse")]
        [FwControllerMethod(Id: "ScLtAAC5s8rr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesRepresentativeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactLogic>(browseRequest);
        }
    }
}
