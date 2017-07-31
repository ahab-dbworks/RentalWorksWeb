using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentalWorksWebLogic.Settings;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class DocumentTypeController : RwDataController
    {
        public DocumentTypeController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype/browse
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype
        [HttpGet]
        public async Task<IActionResult> GetAsync(int pageno, int pagesize)
        {
            return await DoGetAsync<DocumentTypeLogic>(pageno, pagesize, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/documenttype/A0000001
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            return await DoGetAsync<DocumentTypeLogic>(id, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DocumentTypeLogic l)
        {
            return await DoPostAsync<DocumentTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/documenttype/A0000001
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return await DoDeleteAsync(id, typeof(DocumentTypeLogic));
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/documenttype/validateduplicate
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync(ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}