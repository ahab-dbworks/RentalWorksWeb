using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks; 
namespace WebApi.Modules.Settings.DiscountItem 
{ 
[Route("api/v1/[controller]")] 
public class DiscountItemController : AppDataController 
{ 
public DiscountItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { } 
//------------------------------------------------------------------------------------ 
// POST api/v1/discountitem/browse 
[HttpPost("browse")] 
public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest) 
{ 
return await DoBrowseAsync(browseRequest, typeof(DiscountItemLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/discountitem 
[HttpGet] 
public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<DiscountItemLogic>(pageno, pagesize, sort, typeof(DiscountItemLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/discountitem/A0000001 
[HttpGet("{id}")] 
public async Task<IActionResult> GetAsync([FromRoute]string id) 
{ 
return await DoGetAsync<DiscountItemLogic>(id, typeof(DiscountItemLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/discountitem 
[HttpPost] 
public async Task<IActionResult> PostAsync([FromBody]DiscountItemLogic l) 
{ 
return await DoPostAsync<DiscountItemLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/discountitem/A0000001 
[HttpDelete("{id}")] 
public async Task<IActionResult> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync(id, typeof(DiscountItemLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/discountitem/validateduplicate 
[HttpPost("validateduplicate")] 
public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) 
{ 
return await DoValidateDuplicateAsync(request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
