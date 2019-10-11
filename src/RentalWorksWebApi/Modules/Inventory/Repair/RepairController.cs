using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Inventory.Repair
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id:"t4gfyzLkSZhyc")]
    public class RepairController : AppDataController
    {
        public RepairController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(RepairLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"kPJdOLE1mzSJ3")]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx/filedownloadname 
        [HttpPost("exportexcelxlsx/{fileDownloadName}")]
        [FwControllerMethod(Id:"b0WeUj4bhtKca")]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair 
        [HttpGet]
        [FwControllerMethod(Id:"InQYyqMA8dBUA")]
        public async Task<ActionResult<IEnumerable<RepairLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"aK7x7PAZ4fwCT")]
        public async Task<ActionResult<RepairLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair 
        [HttpPost]
        [FwControllerMethod(Id:"qd2ObhYdTAPPI")]
        public async Task<ActionResult<RepairLogic>> PostAsync([FromBody]RepairLogic l)
        {
            return await DoPostAsync<RepairLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair/estimate/A0000001
        [HttpPost("estimate/{id}")]
        [FwControllerMethod(Id:"V6R1MLai1R7Fw")]
        public async Task<ActionResult<ToggleRepairEstimateResponse>> Estimate([FromRoute]string id)
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
                    ToggleRepairEstimateResponse response = await l.ToggleEstimate();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        response.repair = l;
                    }
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/complete/A0000001
        [HttpPost("complete/{id}")]
        [FwControllerMethod(Id:"PgeX6is7sKrYI")]
        public async Task<ActionResult<ToggleRepairCompleteResponse>> Complete([FromRoute]string id)
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
                    ToggleRepairCompleteResponse response = await l.ToggleComplete();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        response.repair = l;
                    }
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/releaseitems/A0000001/4
        [HttpPost("releaseitems/{id}/{quantity}")]
        [FwControllerMethod(Id:"PpSdBovye5sNv")]
        public async Task<ActionResult<RepairReleaseItemsResponse>> ReleaseItems([FromRoute] string id, [FromRoute] int quantity)
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
                    RepairReleaseItemsResponse response = await l.ReleaseItems(quantity);
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        response.repair = l;
                    }
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // POST api/v1/repair/void/A0000001
        [HttpPost("void/{id}")]
        [FwControllerMethod(Id:"AxRbFcXeLZS0a")]
        public async Task<ActionResult<VoidRepairResponse>> Void([FromRoute]string id)
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
                    VoidRepairResponse response = await l.Void();
                    if (response.success)
                    {
                        await l.LoadAsync<RepairLogic>(ids);
                        response.repair = l;
                    }
                    return new OkObjectResult(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------        
        // DELETE api/v1/repair/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"gT2Vr5OjOUHwK")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RepairLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
    }
}
