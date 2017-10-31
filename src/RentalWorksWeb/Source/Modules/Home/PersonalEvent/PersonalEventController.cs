using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using RentalWorksWebApi.Controllers; 
using System.Threading.Tasks; 
namespace RentalWorksWebApi.Modules..PersonalEvent 
{ 
[Route("api/v1/[controller]")] 
public class PersonalEventController : RwDataController 
{ 
public PersonalEventController(IOptions<ApplicationConfig> appConfig) : base(appConfig) { } 
//------------------------------------------------------------------------------------ 
// POST api/v1/personalevent/browse 
[HttpPost("browse")] 
public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequestDto browseRequest) 
{ 
return await DoBrowseAsync(browseRequest, typeof(PersonalEventLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/personalevent 
[HttpGet] 
public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<PersonalEventLogic>(pageno, pagesize, sort, typeof(PersonalEventLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/personalevent/A0000001 
[HttpGet("{id}")] 
public async Task<IActionResult> GetAsync([FromRoute]string id) 
{ 
return await DoGetAsync<PersonalEventLogic>(id, typeof(PersonalEventLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/personalevent 
[HttpPost] 
public async Task<IActionResult> PostAsync([FromBody]PersonalEventLogic l) 
{ 
return await DoPostAsync<PersonalEventLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
//// POST api/v1/personalevent 
//[HttpPost("saveform")] 
//public async Task<IActionResult> SaveFormAsync([FromBody]SaveFormRequest request) 
//{ 
//    return await DoSaveFormAsync<PersonalEventLogic>(request, typeof(PersonalEventLogic)); 
//} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/personalevent/A0000001 
[HttpDelete("{id}")] 
public async Task<IActionResult> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync(id, typeof(PersonalEventLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/personalevent/validateduplicate 
[HttpPost("validateduplicate")] 
public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) 
{ 
return await DoValidateDuplicateAsync(request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
