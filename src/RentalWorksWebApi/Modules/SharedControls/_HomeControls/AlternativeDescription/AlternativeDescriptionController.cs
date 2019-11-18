using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
namespace WebApi.Modules.Home.AlternativeDescription
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "2BkAgaVVrDD3")]
    public class AlternativeDescriptionController : AppDataController
    {
        public AlternativeDescriptionController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(AlternativeDescriptionLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alternativedescription/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "2CG5lxPKl8p")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alternativedescription/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "2CYIfkx3Khqk")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alternativedescription 
        [HttpGet]
        [FwControllerMethod(Id: "2dgCcqwYO3S")]
        public async Task<ActionResult<IEnumerable<AlternativeDescriptionLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<AlternativeDescriptionLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/alternativedescription/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "2eV6NL90mo8t")]
        public async Task<ActionResult<AlternativeDescriptionLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<AlternativeDescriptionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/alternativedescription 
        [HttpPost]
        [FwControllerMethod(Id: "2f900uBiUOVaw")]
        public async Task<ActionResult<AlternativeDescriptionLogic>> PostAsync([FromBody]AlternativeDescriptionLogic l)
        {
            return await DoPostAsync<AlternativeDescriptionLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/alternativedescription/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id: "2htGFRkQyiLj")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<AlternativeDescriptionLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
