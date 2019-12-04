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

namespace WebApi.Modules.Home.InventorySearch
{
    //------------------------------------------------------------------------------------ 
    public class InventorySearchGetTotalRequest
    {
        public string SessionId;
        public string OrderId;
    }
    public class InventorySearchGetTotalResponse: TSpStatusResponse
    {
        public decimal? TotalQuantityInSession;
    }

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
        public bool? ShowAvailability;
        public DateTime FromDate;
        public DateTime ToDate;
        public bool? ShowImages;
        public bool? HideInventoryWithZeroQuantity;
        public string SortBy;
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAccessoriesRequest
    {
        public string SessionId;
        public string OrderId;
        public string ParentId;
        public string GrandParentId;
        public string WarehouseId;
        public bool? ShowAvailability;
        public DateTime FromDate;
        public DateTime ToDate;
        public bool? ShowImages;
    }
    //------------------------------------------------------------------------------------ 
    public class InventorySearchAddToRequest
    {
        public string SessionId;
        public string OrderId;
        public string InventoryId;
    }
    //------------------------------------------------------------------------------------ 


    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"i4xC0FYgT9qo")]
    public class InventorySearchController : AppDataController
    {
        //------------------------------------------------------------------------------------ 
        public InventorySearchController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(InventorySearchLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/search
        [HttpPost("search")]
        [FwControllerMethod(Id:"obY8YJeiKDfb")]
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
        [FwControllerMethod(Id:"h6KcF18iVMSP")]
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
        [FwControllerMethod(Id:"pIzlx22ziLGg", ActionType: FwControllerActionTypes.New)]
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
        [FwControllerMethod(Id:"Sn1Xo2Zd4M")]
        public async Task<ActionResult<InventorySearchGetTotalResponse>> GetTotal([FromRoute]string sessionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //InventorySearchGetTotalRequest request = new InventorySearchGetTotalRequest();
                //request.SessionId = sessionId;
                //InventorySearchLogic l = new InventorySearchLogic();
                //l.SetDependencies(this.AppConfig, this.UserSession);
                //InventorySearchGetTotalResponse response = await l.GetTotalAsync(request);
                //return new OkObjectResult(response);

                InventorySearchGetTotalResponse response = await InventorySearchFunc.GetTotalAsync(AppConfig, UserSession, sessionId);
                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/inventorysearch/addto 
        [HttpPost("addto")]
        [FwControllerMethod(Id:"bB1lEAjR2sZy")]
        public async Task<ActionResult<bool>> AddTo([FromBody]InventorySearchAddToRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //InventorySearchLogic l = new InventorySearchLogic();
                //l.SetDependencies(this.AppConfig, this.UserSession);
                //bool b = await l.AddToAsync(processRequest);
                //return new OkObjectResult(b);
                bool b = await InventorySearchFunc.AddToAsync(AppConfig, UserSession, request);
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
