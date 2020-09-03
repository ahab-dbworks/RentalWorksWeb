using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Modules.Administrator.EmailTemplate;

namespace WebApi.Modules.Administrator.EmailTemplate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id: "PMAan9TbkIOf")]
    public class EmailTemplateController : AppDataController
    {
        public EmailTemplateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(EmailTemplateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "M5KpDuUK12sA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            browseRequest.top = 100;
            browseRequest.searchfields.Add("EmailType");
            browseRequest.searchfieldvalues.Add("EMAIL");
            browseRequest.searchfieldoperators.Add("=");
            browseRequest.searchfields.Add("Inactive");
            browseRequest.searchfieldvalues.Add("T");
            browseRequest.searchfieldoperators.Add("<>");
            //browseRequest.searchfields.Add("FilterId");   - (JG) RWW templates do not have a filterId attached to them like in GWW, leaving this here in case that is changed in the future.
            //browseRequest.searchfieldvalues.Add(browseRequest.filterfields["CampusId"]);
            //browseRequest.searchfieldoperators.Add("=");
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate/exportexcelxlsx
        //[HttpPost("exportexcelxlsx")]
        //[FwControllerMethod(Id: "c09OYeaek0YF", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        //{
        //    return await DoExportExcelXlsxFileAsync(browseRequest);
        //}
        ////------------------------------------------------------------------------------------ 
        //// GET api/v1/emailtemplate 
        //[HttpGet]
        //[FwControllerMethod(Id: "qYlFH0BWlnLI", ActionType: FwControllerActionTypes.Browse)]
        //public async Task<ActionResult<IEnumerable<EmailTemplateLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<EmailTemplateLogic>(pageno, pagesize, sort);
        //}
        //------------------------------------------------------------------------------------ 
        // GET api/v1/emailtemplate/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "bj2QaFoFDDkP", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<EmailTemplateLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<EmailTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate 
        [HttpPost]
        [FwControllerMethod(Id: "D5wakhCv1l0X", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<EmailTemplateLogic>> NewAsync([FromBody]EmailTemplateLogic l)
        {
            return await DoNewAsync<EmailTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate/getbodyformat 
        //[HttpPost]
        //[FwControllerMethod(Id: "EXWv606UqVn3", ActionType: FwControllerActionTypes.New)]
        //public async Task<ActionResult<EmailTemplateLogic>> NewAsync([FromBody]EmailTemplateLogic l)
        //{
        //    return await DoNewAsync<EmailTemplateLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/emailtemplate/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "ox5DdUHWv98e", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<EmailTemplateLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/emailtemplate/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "hdaPvlMlU5VI", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<EmailTemplateLogic>> EditAsync([FromRoute] string id, [FromBody]EmailTemplateLogic l)
        {
            return await DoEditAsync<EmailTemplateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate/templatecategories 
        [HttpPost("templatecategories")]
        [FwControllerMethod(Id: "7UASz6J2seCN", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<EmailTemplateLogic.TemplateCategoriesResponse>> GetTemplateCategoriesAsync([FromBody]EmailTemplateLogic l)
        {
            var et = FwBusinessLogic.CreateBusinessLogic<EmailTemplateLogic>(this.AppConfig, this.UserSession);
            return await et.GetTemplateCategoriesAsync();
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/emailtemplate/templatefields 
        [HttpPost("templatefields")]
        [FwControllerMethod(Id: "JTBR5xvD6s3g", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<EmailTemplateLogic.TemplateFieldsResponse>> GetTemplateFieldsAsync([FromBody]EmailTemplateLogic.GetTemplateFieldsRequest l)
        {
            var et = FwBusinessLogic.CreateBusinessLogic<EmailTemplateLogic>(this.AppConfig, this.UserSession);
            return await et.GetTemplateFieldsAsync(l);
        }
    }
}
