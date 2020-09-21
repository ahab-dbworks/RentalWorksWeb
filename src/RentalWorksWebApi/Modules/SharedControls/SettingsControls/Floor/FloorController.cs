using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Settings.FacilitySettings.Building;
using System;

namespace WebApi.Modules.Settings.Floor
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"LrybQVClgY6f")]
    public class FloorController : AppDataController
    {
        public FloorController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(FloorLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"I5XCMOmSoDZ5", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"yK5WFt4LwLJi", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor 
        [HttpGet]
        [FwControllerMethod(Id:"eBU90uF94BeU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<FloorLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<FloorLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/floor/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"tkeJBbqpX9ms", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FloorLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<FloorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor 
        [HttpPost]
        [FwControllerMethod(Id:"Oh83FN8lsl0D", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<FloorLogic>> NewAsync([FromBody]FloorLogic l)
        {
            return await DoNewAsync<FloorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/floor/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "CtOAuShIlVKxp", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<FloorLogic>> EditAsync([FromRoute] string id, [FromBody]FloorLogic l)
        {
            return await DoEditAsync<FloorLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/floor/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"WlKt8i6QIrfs", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<FloorLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/floor/sort
        [HttpPost("sort")]
        [FwControllerMethod(Id: "wValjlNn7U9RI", ActionType: FwControllerActionTypes.Option)]
        public async Task<ActionResult<SortItemsResponse>> SortFloorsAsync([FromBody]SortFloorsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return await BuildingFunc.SortFloors(AppConfig, UserSession, request);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
