using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks; 
namespace RentalWorksWebApi.Modules..AppDocument 
{ 
[Route("api/v1/[controller]")] 
public class AppDocumentController : RwDataController 
{ 
public AppDocumentController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { } 
//------------------------------------------------------------------------------------ 
// POST api/v1/appdocument/browse 
[HttpPost("browse")] 
public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest) 
{ 
return await DoBrowseAsync(browseRequest, typeof(AppDocumentLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/appdocument 
[HttpGet] 
public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<AppDocumentLogic>(pageno, pagesize, sort, typeof(AppDocumentLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/appdocument/A0000001 
[HttpGet("{id}")] 
public async Task<IActionResult> GetAsync([FromRoute]string id) 
{ 
return await DoGetAsync<AppDocumentLogic>(id, typeof(AppDocumentLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/appdocument 
[HttpPost] 
public async Task<IActionResult> PostAsync([FromBody]AppDocumentLogic l) 
{ 
return await DoPostAsync<AppDocumentLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
//// POST api/v1/appdocument 
//[HttpPost("saveform")] 
//public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
//{ 
//    return await DoSaveFormAsync<AppDocumentLogic>(request, typeof(AppDocumentLogic)); 
//} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/appdocument/A0000001 
[HttpDelete("{id}")] 
public async Task<IActionResult> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync(id, typeof(AppDocumentLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/appdocument/validateduplicate 
[HttpPost("validateduplicate")] 
public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) 
{ 
return await DoValidateDuplicateAsync(request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
