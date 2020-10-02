using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using System;
//using Microsoft.AspNetCore.Http;
using FwStandard.SqlServer;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.InventorySearch
{
    //------------------------------------------------------------------------------------ 
    public class InventorySearchGetTotalRequest
    {
        public string SessionId { get; set; }
        public string OrderId { get; set; }
    }
    public class InventorySearchGetTotalResponse : TSpStatusResponse
    {
        public decimal? TotalQuantityInSession { get; set; }
    }

    //------------------------------------------------------------------------------------ 
    public class InventorySearchRequest
    {
        public string SessionId { get; set; }
        public string OrderId { get; set; }
        public string AvailableFor { get; set; }
        public string WarehouseId { get; set; }
        public string CurrencyId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string Classification { get; set; }
        public string SearchText { get; set; }
        public string AttributeId { get; set; }
        public string AttributeValueId { get; set; }
        public string AttributeValueRange { get; set; }
        public bool? ShowAvailability { get; set; }
        public DateTime FromDate { get; set; }
        public string FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public string ToTime { get; set; }
        public bool? ShowImages { get; set; }
        public bool? HideInventoryWithZeroQuantity { get; set; }
        public string SortBy { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAccessoriesRequest
    {
        public string SessionId { get; set; }
        public string OrderId { get; set; }
        public string ParentId { get; set; }
        public string GrandParentId { get; set; }
        public string WarehouseId { get; set; }
        public bool? ShowAvailability { get; set; }
        public DateTime FromDate { get; set; }
        public string FromTime { get; set; }
        public DateTime ToDate { get; set; }
        public string ToTime { get; set; }
        public bool? ShowImages { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public abstract class InventorySearchAddToRequest
    {
        public string SessionId { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAddToOrderRequest : InventorySearchAddToRequest
    {
        public string OrderId { get; set; }
        public int? InsertAtIndex { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAddToCompleteKitContainerRequest : InventorySearchAddToRequest
    {
        public string InventoryId { get; set; }
    }
    //------------------------------------------------------------------------------------ 


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "i4xC0FYgT9qo")]
    public class InventorySearchController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventorySearchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySearchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/search
        [HttpPost("search")]
        [FwControllerMethod(Id: "obY8YJeiKDfb")]
        public async Task<ActionResult<FwJsonDataTable>> SearchAsync([FromBody]InventorySearchRequest searchRequest)
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/accessories
        [HttpPost("accessories")]
        [FwControllerMethod(Id: "h6KcF18iVMSP")]
        public async Task<ActionResult<FwJsonDataTable>> SearchAccessoriesAsync([FromBody]InventorySearchAccessoriesRequest searchRequest)
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
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------         
        // POST api/v1/inventorysearch 
        [HttpPost]
        [FwControllerMethod(Id: "pIzlx22ziLGg", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<InventorySearchLogic>> PostAsync([FromBody]InventorySearchLogic l)
        {
            return await DoPostAsync<InventorySearchLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/inventorysearch/A0000001
        //[HttpPut("{id}")]
        //[FwControllerMethod(Id: "F09OEvjou3KIm", ActionType: FwControllerActionTypes.Edit)]
        //public async Task<ActionResult<InventorySearchLogic>> EditAsync([FromRoute] string id, [FromBody]InventorySearchLogic l)
        //{
        //    return await DoEditAsync<InventorySearchLogic>(l);
        //}
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/gettotal
        [HttpGet("gettotal/{sessionid}")]
        [FwControllerMethod(Id: "Sn1Xo2Zd4M")]
        public async Task<ActionResult<InventorySearchGetTotalResponse>> GetTotal([FromRoute]string sessionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InventorySearchGetTotalResponse response = await InventorySearchFunc.GetTotalAsync(AppConfig, UserSession, sessionId);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/addtoorder 
        [HttpPost("addtoorder")]
        [FwControllerMethod(Id: "bB1lEAjR2sZy")]
        public async Task<ActionResult<bool>> AddToOrder([FromBody]InventorySearchAddToOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool b = await InventorySearchFunc.AddToOrderAsync(AppConfig, UserSession, request);
                return new OkObjectResult(b);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/addtopackage 
        [HttpPost("addtopackage")]
        [FwControllerMethod(Id: "HvTKFTiLaGJr0")]
        public async Task<ActionResult<bool>> AddToPackage([FromBody]InventorySearchAddToCompleteKitContainerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool b = await InventorySearchFunc.AddToPackageAsync(AppConfig, UserSession, request);
                return new OkObjectResult(b);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
