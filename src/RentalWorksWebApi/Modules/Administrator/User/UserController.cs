using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks;
namespace RentalWorksWebApi.Modules.Administrator.User
{
    [Route("api/v1/[controller]")]
    public class UserController : RwDataController
    {
        public UserController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user 
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserLogic>(pageno, pagesize, sort, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserLogic>(id, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserLogic l)
        {
            return await DoPostAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/user/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(UserLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}