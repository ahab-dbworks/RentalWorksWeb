using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.PersonnelType
{
    [Route("api/v1/[controller]")]
    public class PersonnelTypeController : AppDataController
    {
        public PersonnelTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/personneltype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(PersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/personneltype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<PersonnelTypeLogic>(pageno, pagesize, sort, typeof(PersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/personneltype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<PersonnelTypeLogic>(id, typeof(PersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/personneltype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PersonnelTypeLogic l)
        {
            return await DoPostAsync<PersonnelTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/personneltype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(PersonnelTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/personneltype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}