using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.CompanyTaxOption
{
    [Route("api/v1/[controller]")]
    public class CompanyTaxOptionController : RwDataController
    {
        public CompanyTaxOptionController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(CompanyTaxOptionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(pageno, pagesize, sort, typeof(CompanyTaxOptionLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/companytaxoption/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<CompanyTaxOptionLogic>(id, typeof(CompanyTaxOptionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CompanyTaxOptionLogic l)
        {

            // NOTE: insert is currently not supported becase there is no surrogate key in this module

            return await DoPostAsync<CompanyTaxOptionLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/companytaxoption/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(CompanyTaxOptionLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/companytaxoption/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}