using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
namespace WebApi.Modules.Home.ProjectContact
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"ZvjyLW5OM5s1X")]
    public class ProjectContactController : AppDataController
    {
        public ProjectContactController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProjectContactLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcontact/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"3rZGoq3npczY9")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"Lq1MHWWQ6G0Up")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact 
        [HttpGet]
        [FwControllerMethod(Id:"lhYQwqofmZe90")]
        public async Task<ActionResult<IEnumerable<ProjectContactLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ProjectContactLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/projectcontact/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"EKmFrQwUT8wBK")]
        public async Task<ActionResult<ProjectContactLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProjectContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/projectcontact 
        [HttpPost]
        [FwControllerMethod(Id:"iAiORZC4GH1Ud")]
        public async Task<ActionResult<ProjectContactLogic>> PostAsync([FromBody]ProjectContactLogic l)
        {
            return await DoPostAsync<ProjectContactLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/projectcontact/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"8fqbgZRvPN9wr")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<ProjectContactLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
