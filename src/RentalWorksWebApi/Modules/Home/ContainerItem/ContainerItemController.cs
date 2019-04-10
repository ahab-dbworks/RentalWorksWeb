using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebLibrary;
using System;
using Microsoft.AspNetCore.Http;
using WebApi.Logic;

namespace WebApi.Modules.Home.ContainerItem
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"2ZVm2vAYTYJC")]
    public class ContainerItemController : AppDataController
    {
        public ContainerItemController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(ContainerItemLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"iQeXRj8im3k2")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/legend 
        [HttpGet("legend")]
        [FwControllerMethod(Id: "28ypADGp8lJRl")]
        public async Task<ActionResult<Dictionary<string, string>>> GetLegend()
        {
            Dictionary<string, string> legend = new Dictionary<string, string>();
            legend.Add("Ready", RwGlobals.CONTAINER_READY_COLOR);
            legend.Add("Incomplete", RwGlobals.CONTAINER_INCOMPLETE_COLOR);
            await Task.CompletedTask; // get rid of the no async call warning
            return new OkObjectResult(legend);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"hq7a4e3uXtez")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem 
        [HttpGet]
        [FwControllerMethod(Id:"Yb3dL1hmjU37")]
        public async Task<ActionResult<IEnumerable<ContainerItemLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<ContainerItemLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/containeritem/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"t1bsMSPUNPNx")]
        public async Task<ActionResult<ContainerItemLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<ContainerItemLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem 
        [HttpPost]
        [FwControllerMethod(Id:"74qvuZNaDoEB")]
        public async Task<ActionResult<ContainerItemLogic>> PostAsync([FromBody]ContainerItemLogic l)
        {
            return await DoPostAsync<ContainerItemLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/containeritem/emptycontainer/A0000001 
        [HttpPost("emptycontainer/{id}")]
        [FwControllerMethod(Id: "bBSKLVtzUKn")]
        public async Task<ActionResult<EmptyContainerItemResponse>> Empty([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                ContainerItemLogic container = new ContainerItemLogic();
                container.SetDependencies(AppConfig, UserSession);
                if (await container.LoadAsync<ContainerItemLogic>(ids))
                {
                    EmptyContainerItemResponse response = await ContainerItemFunc.EmptyContainer(AppConfig, UserSession, id);
                    return response;
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
        //// DELETE api/v1/containeritem/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id:"WhehdFIg4Y8a")]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync(id);
        //}
        ////------------------------------------------------------------------------------------ 
    }
}
