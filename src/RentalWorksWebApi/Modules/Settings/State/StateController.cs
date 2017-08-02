using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.State
{
    [Route("api/v1/[controller]")]
    public class StateController : RwDataController
    {
        public StateController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/browse
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<StateLogic>(pageno, pagesize, sort, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]string id)
        {
            return await DoGetAsync<StateLogic>(id, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StateLogic l)
        {
            return await DoPostAsync<StateLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/State/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicate([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}