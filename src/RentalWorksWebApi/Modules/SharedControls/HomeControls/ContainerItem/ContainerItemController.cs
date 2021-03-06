using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.HomeControls.ContainerItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "2ZVm2vAYTYJC")]
    public class ContainerItemController : AppDataController
    {
        public ContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "iQeXRj8im3k2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "28ypADGp8lJRl", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Ready", RwGlobals.CONTAINER_READY_COLOR);
            legend.Add("Incomplete", RwGlobals.CONTAINER_INCOMPLETE_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "hq7a4e3uXtez", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        //// GET api/v1/containeritem 
        //[HttpGet]
        //[FwControllerMethod(Id: "Yb3dL1hmjU37", ActionType: FwControllerActionTypes.View)]
        //public async Task<ActionResult<IEnumerable<ContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        //{
        //    return await DoGetAsync<ContainerItemLogic>(pageno, pagesize, sort);
        //}
        ////------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "t1bsMSPUNPNx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<ContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem 
        [HttpPost]
        [FwControllerMethod(Id: "74qvuZNaDoEB", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<ContainerItemLogic>> NewAsync([FromBody]ContainerItemLogic l)
        {
            return await DoNewAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/containeritem/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "d53rmAOM2slgD", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<ContainerItemLogic>> EditAsync([FromRoute] string id, [FromBody]ContainerItemLogic l)
        {
            return await DoEditAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/instantiatecontainer
        [HttpPost("instantiatecontainer")]
        [FwControllerMethod(Id: "lgKt86qdjuNJ5")]
        public async Task<ActionResult<InstantiateContainerItemResponse>> Instantiate([FromBody]InstantiateContainerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                InstantiateContainerItemResponse response = await ContainerItemFunc.InstantiateContainer(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/emptycontainer
        [HttpPost("emptycontainer")]
        [FwControllerMethod(Id: "bBSKLVtzUKn")]
        public async Task<ActionResult<EmptyContainerItemResponse>> Empty([FromBody]EmptyContainerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmptyContainerItemResponse response = await ContainerItemFunc.EmptyContainer(AppConfig, UserSession, request);
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/removefromcontainer 
        [HttpPost("removefromcontainer")]
        [FwControllerMethod(Id: "d3Xu2IaPYpq")]
        public async Task<ActionResult<RemoveFromContainerResponse>> Remove([FromBody]RemoveFromContainerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                RemoveFromContainerResponse response = new RemoveFromContainerResponse();
                if ((string.IsNullOrEmpty(request.ItemId)) && (string.IsNullOrEmpty(request.InventoryId)))
                {
                    response.success = false;
                    response.msg = "Bar Code or I-Code must be specified when removing items from a Container.";
                }
                else if ((string.IsNullOrEmpty(request.ContainerItemId)) && (string.IsNullOrEmpty(request.ItemId)))
                {
                    response.success = false;
                    response.msg = "Container Item is required when removing items tracked by Quantity.";
                }
                else
                {
                    response = await ContainerItemFunc.RemoveFromContainer(AppConfig, UserSession, request);
                }
                return response;
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/containeritem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"WhehdFIg4Y8a", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<ContainerItemLogic>(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
