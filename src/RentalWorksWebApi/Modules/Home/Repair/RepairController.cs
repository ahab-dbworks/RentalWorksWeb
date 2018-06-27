using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;

namespace WebApi.Modules.Home.Repair
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    public class RepairController : AppDataController
    {
        public RepairController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair/browse 
        [HttpPost("browse")]
        public async Task<IActionResult> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest, typeof(RepairLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        public async Task<IActionResult> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair 
        [HttpGet]
        public async Task<IActionResult> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairLogic>(pageno, pagesize, sort, typeof(RepairLogic));
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair/A0000001 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairLogic>(id, typeof(RepairLogic));
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RepairLogic l)
        {
            return await DoPostAsync<RepairLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair/estimate/A0000001
        [HttpPost("estimate/{id}")]
        public async Task<IActionResult> Estimate([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                RepairLogic l = new RepairLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<RepairLogic>(ids))
                {
                    TSpStatusReponse response = await l.ToggleEstimate();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/complete/A0000001
        [HttpPost("complete/{id}")]
        public async Task<IActionResult> Complete([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                RepairLogic l = new RepairLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<RepairLogic>(ids))
                {
                    TSpStatusReponse response = await l.ToggleComplete();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/releaseitems/A0000001/4
        [HttpPost("releaseitems/{id}/{quantity}")]
        public async Task<IActionResult> ReleaseItems([FromRoute] string id, [FromRoute] int quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                RepairLogic l = new RepairLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<RepairLogic>(ids))
                {
                    TSpStatusReponse response = await l.ReleaseItems(quantity);
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/void/A0000001
        [HttpPost("void/{id}")]
        public async Task<IActionResult> Void([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                RepairLogic l = new RepairLogic();
                l.SetDependencies(AppConfig, UserSession);
                if (await l.LoadAsync<RepairLogic>(ids))
                {
                    TSpStatusReponse response = await l.Void();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        return new OkObjectResult(l);
                    }
                    else
                    {
                        throw new Exception(response.msg);
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                FwApiException jsonException = new FwApiException();
                jsonException.StatusCode = StatusCodes.Status500InternalServerError;
                jsonException.Message = ex.Message;
                jsonException.StackTrace = ex.StackTrace;
                return StatusCode(jsonException.StatusCode, jsonException);
            }
        }
        //------------------------------------------------------------------------------------        
        // DELETE api/v1/repair/A0000001 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync(id, typeof(RepairLogic));
        }
        //------------------------------------------------------------------------------------ 
    }
}
