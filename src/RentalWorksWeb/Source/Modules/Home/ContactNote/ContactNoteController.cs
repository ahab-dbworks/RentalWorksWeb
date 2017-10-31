using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks; 
namespace RentalWorksWebApi.Modules..ContactNote 
{ 
[Route("api/v1/[controller]")] 
public class ContactNoteController : RwDataController 
{ 
public ContactNoteController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { } 
//------------------------------------------------------------------------------------ 
// POST api/v1/contactnote/browse 
[HttpPost("browse")] 
public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest) 
{ 
return await DoBrowseAsync(browseRequest, typeof(ContactNoteLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/contactnote 
[HttpGet] 
public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<ContactNoteLogic>(pageno, pagesize, sort, typeof(ContactNoteLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/contactnote/A0000001 
[HttpGet("{id}")] 
public async Task<IActionResult> GetAsync([FromRoute]string id) 
{ 
return await DoGetAsync<ContactNoteLogic>(id, typeof(ContactNoteLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/contactnote 
[HttpPost] 
public async Task<IActionResult> PostAsync([FromBody]ContactNoteLogic l) 
{ 
return await DoPostAsync<ContactNoteLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
//// POST api/v1/contactnote 
//[HttpPost("saveform")] 
//public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
//{ 
//    return await DoSaveFormAsync<ContactNoteLogic>(request, typeof(ContactNoteLogic)); 
//} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/contactnote/A0000001 
[HttpDelete("{id}")] 
public async Task<IActionResult> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync(id, typeof(ContactNoteLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/contactnote/validateduplicate 
[HttpPost("validateduplicate")] 
public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) 
{ 
return await DoValidateDuplicateAsync(request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
