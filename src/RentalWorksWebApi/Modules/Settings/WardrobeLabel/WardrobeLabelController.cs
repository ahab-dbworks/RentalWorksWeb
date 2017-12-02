using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebApi.Controllers;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Modules.Settings.WardrobeLabel
{
    [Route("api/v1/[controller]")]
    public class WardrobeLabelController : RwDataController
    {
        public WardrobeLabelController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobelabel/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(WardrobeLabelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobelabel
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WardrobeLabelLogic>(pageno, pagesize, sort, typeof(WardrobeLabelLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/wardrobelabel/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]string id)
        {
            return await DoGetAsync<WardrobeLabelLogic>(id, typeof(WardrobeLabelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobelabel
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WardrobeLabelLogic l)
        {
            return await DoPostAsync<WardrobeLabelLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/wardrobelabel/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(WardrobeLabelLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/wardrobelabel/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}