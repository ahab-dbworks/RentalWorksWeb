using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PropsCondition
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class PropsConditionController : AppDataController
    {
        public PropsConditionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(PropsConditionLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition/browse
        [HttpPost("browse")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition
        [HttpGet]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<PropsConditionLogic>(pageno, pagesize, sort, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/propscondition/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<PropsConditionLogic>(id, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/propscondition
        [HttpPost]
        [Authorize(Policy = "")]
        public async Task<IActionResult> PostAsync([FromBody]PropsConditionLogic l)
        {
            return await DoPostAsync<PropsConditionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/propscondition/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(PropsConditionLogic));
        }
        //------------------------------------------------------------------------------------
    }
}