using FwStandard.AppManager;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Utilities.ProgressMeter
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id:"Up5tRDyxq02LA")]
    public class ProgressMeterController : AppDataController
    {
        public ProgressMeterController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProgressMeterLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/progressmeter/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"OdtH92OLydfn8")]
        public async Task<ActionResult<ProgressMeterLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProgressMeterLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
