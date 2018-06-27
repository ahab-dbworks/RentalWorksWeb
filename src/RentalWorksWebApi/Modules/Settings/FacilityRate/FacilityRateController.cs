using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityRate
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FacilityRateController : AppDataController
    {
        public FacilityRateController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityRateLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/facilityrate/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityRateLogic>(pageno, pagesize, sort, typeof(FacilityRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/facilityrate/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityRateLogic>(id, typeof(FacilityRateLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/facilityrate 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FacilityRateLogic l)
        {
            return await DoPostAsync<FacilityRateLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/facilityrate/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityRateLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
} 
