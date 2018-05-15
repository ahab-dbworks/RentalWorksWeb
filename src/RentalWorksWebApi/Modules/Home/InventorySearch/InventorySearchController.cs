using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Home.PickList;
using System;
using Microsoft.AspNetCore.Http;
using FwStandard.SqlServer;

namespace WebApi.Modules.Home.InventorySearch
{
    //------------------------------------------------------------------------------------ 
    public class InventorySearchRequest
    {
        public string SessionId;
        public string OrderId;
        public string AvailableFor;
        public string WarehouseId;
        public string InventoryTypeId;
        public string CategoryId;
        public string SubCategoryId;
        public string Classification;
        public string SearchText;
        public bool ShowAvailability;
        public DateTime FromDate;
        public DateTime ToDate;
        public bool ShowImages;
        public string SortBy;
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAccessoriesRequest
    {
        public string SessionId;
        public string OrderId;
        public string ParentId;
        public string WarehouseId;
        public bool ShowAvailability;
        public DateTime FromDate;
        public DateTime ToDate;
        public bool ShowImages;
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchPreviewRequest
    {
        public string SessionId;
        public bool ShowAvailability;
        public DateTime FromDate;
        public DateTime ToDate;
    }
    //------------------------------------------------------------------------------------ 

    [Route("api/v1/[controller]")]
    public class InventorySearchController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventorySearchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/search
        [HttpPost("search")]
        public async Task<IActionResult> SearchAsync([FromBody]InventorySearchRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchLogic l = new InventorySearchLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.SearchAsync(searchRequest);
                return new OkObjectResult(dt);
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
        // POST api/v1/inventorysearch/accessories
        [HttpPost("accessories")]
        public async Task<IActionResult> SearchAccessoriesAsync([FromBody]InventorySearchAccessoriesRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchLogic l = new InventorySearchLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.SearchAccessoriesAsync(searchRequest);
                return new OkObjectResult(dt);
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
        //------------------------------------------------------------------------------------         // POST api/v1/inventorysearch 
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]InventorySearchLogic l)
        {
            return await DoPostAsync<InventorySearchLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/preview
        [HttpPost("preview")]
        public async Task<IActionResult> PreviewAsync([FromBody]InventorySearchPreviewRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchLogic l = new InventorySearchLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                FwJsonDataTable dt = await l.PreviewAsync(searchRequest);
                return new OkObjectResult(dt);
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
        // POST api/v1/inventorysearch/addto 
        [HttpPost("addto")]
        public async Task<IActionResult> AddTo([FromBody]InventorySearchRequest processRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchLogic l = new InventorySearchLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                bool b = await l.AddToAsync(processRequest);
                return new OkObjectResult(b);
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
        // POST api/v1/inventorysearch/validateduplicate 
        [HttpPost("validateduplicate")]
        public async Task<IActionResult> ValidateDuplicateAsync([FromBody]ValidateDuplicateRequest request)
        {
            return await DoValidateDuplicateAsync(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
