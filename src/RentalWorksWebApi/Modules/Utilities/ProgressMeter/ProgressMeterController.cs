using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;

namespace WebApi.Modules.Utilities.ProgressMeter
{
    [Route("api/v1/[controller]")]
    public class ProgressMeterController : AppDataController
    {
        public ProgressMeterController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ProgressMeterLogic); }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/progressmeter/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<ProgressMeterLogic>(id, typeof(ProgressMeterLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}