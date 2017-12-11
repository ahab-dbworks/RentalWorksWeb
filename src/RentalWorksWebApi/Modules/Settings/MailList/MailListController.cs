using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.MailList
{
    [Route("api/v1/[controller]")]
    public class MailListController : AppDataController
    {
        public MailListController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<MailListLogic>(pageno, pagesize, sort, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/maillist/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<MailListLogic>(id, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]MailListLogic l)
        {
            return await DoPostAsync<MailListLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/maillist/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(MailListLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/maillist/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}