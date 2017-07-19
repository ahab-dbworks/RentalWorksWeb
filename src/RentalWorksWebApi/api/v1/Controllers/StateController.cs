using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
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
        public async Task<IActionResult> Get(int pageno, int pagesize)
        {
            return await DoGetAsync<StateLogic>(pageno, pagesize, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/State/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
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
        public async Task<IActionResult> Delete(string id)
        {
            return await DoDeleteAsync(id, typeof(StateLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/State/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicate(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}