using FwStandard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;

namespace WebApi.Modules.Settings.DocumentType
{
    [Route("api/v1/[controller]")]
    public class DocumentTypeController : AppDataController
    {
        public DocumentTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype/browse
        [HttpPost("browse")]
        [Authorize(Policy = "{B1C7EC8C-A38B-4B88-A635-77106140632D}")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype
        [HttpGet]
        [Authorize(Policy = "{C819F03A-4EE9-482C-8157-B2477DA26DBF}")]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize, string sort)
        {
            return await DoGetAsync<DocumentTypeLogic>(pageno, pagesize, sort, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype/A0000001
        [HttpGet("{id}")]
        [Authorize(Policy = "{64BC04C7-CE73-4124-A138-C51FC5BB064C}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<DocumentTypeLogic>(id, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype
        [HttpPost]
        [Authorize(Policy = "{B13DD731-3955-4FA5-AD32-EE92994CAF7B}")]
        public async Task<IActionResult> PostAsync([FromBody]DocumentTypeLogic l)
        {
            return await DoPostAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/documenttype/A0000001
        [HttpDelete("{id}")]
        [Authorize(Policy = "{7EA7F5A5-CD3E-40E8-88B5-74690E33F0D8}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype/validateduplicate
        [HttpPost("validateduplicate")]
        [Authorize(Policy = "{276E8939-070A-4876-AE25-EF41C352C845}")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}