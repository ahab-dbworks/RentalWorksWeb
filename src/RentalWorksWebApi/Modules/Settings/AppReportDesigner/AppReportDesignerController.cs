using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks; 
namespace WebApi.Modules.Settings.AppReportDesigner 
{ 
[Route("api/v1/[controller]")] 
public class AppReportDesignerController : AppDataController 
{ 
public AppReportDesignerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { } 
//------------------------------------------------------------------------------------ 
// POST api/v1/appreportdesigner/browse 
[HttpPost("browse")] 
public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest) 
{ 
return await DoBrowseAsync(browseRequest, typeof(AppReportDesignerLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/appreportdesigner 
[HttpGet] 
public async Task<IActionResult> GetAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort) 
{ 
return await DoGetAsync<AppReportDesignerLogic>(pageno, pagesize, sort, typeof(AppReportDesignerLogic)); 
} 
//------------------------------------------------------------------------------------ 
// GET api/v1/appreportdesigner/A0000001 
[HttpGet("{id}")] 
public async Task<IActionResult> GetAsync([FromRoute]string id) 
{ 
return await DoGetAsync<AppReportDesignerLogic>(id, typeof(AppReportDesignerLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/appreportdesigner 
[HttpPost] 
public async Task<IActionResult> PostAsync([FromBody]AppReportDesignerLogic l) 
{ 
return await DoPostAsync<AppReportDesignerLogic>(l); 
} 
//------------------------------------------------------------------------------------ 
// DELETE api/v1/appreportdesigner/A0000001 
[HttpDelete("{id}")] 
public async Task<IActionResult> DeleteAsync([FromRoute]string id) 
{ 
return await DoDeleteAsync(id, typeof(AppReportDesignerLogic)); 
} 
//------------------------------------------------------------------------------------ 
// POST api/v1/appreportdesigner/validateduplicate 
[HttpPost("validateduplicate")] 
public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request) 
{ 
return await DoValidateDuplicateAsync(request); 
} 
//------------------------------------------------------------------------------------ 
} 
} 
