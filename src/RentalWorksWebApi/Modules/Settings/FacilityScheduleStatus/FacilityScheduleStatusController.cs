using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.FacilityScheduleStatus
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    public class FacilityScheduleStatusController : AppDataController
    {
        public FacilityScheduleStatusController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FacilityScheduleStatusLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{EE95A8B5-B9E7-4C19-8085-286D7E85F7F9}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus
        [HttpGet]
        [Authorize(Policy = "{196E7BFC-8E31-42E3-A776-4E8B2B66AD47}")]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(pageno, pagesize, sort, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/facilityschedulestatus/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{17E60B4C-49C0-45DB-B9A5-1E7805A05AC2}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<FacilityScheduleStatusLogic>(id, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/facilityschedulestatus
        [HttpPost]
        [Authorize(Policy = "{260ECC71-B40A-4626-9B3C-F6FBA00F4EBF}")]
        public async Task<IActionResult> PostAsync([FromBody]FacilityScheduleStatusLogic l)
        {
            return await DoPostAsync<FacilityScheduleStatusLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/facilityschedulestatus/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{2AC9A850-6CCF-4997-ADC7-B125A9887CAC}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(FacilityScheduleStatusLogic));
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------
    }
}