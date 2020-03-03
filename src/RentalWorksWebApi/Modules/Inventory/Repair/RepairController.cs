using FwStandard.AppManager;
using FwStandard.Models;
using FwStandard.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Inventory.Asset;
using WebApi.Modules.Inventory.RentalInventory;
using WebApi.Modules.Inventory.SalesInventory;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Settings.RepairSettings.RepairItemStatus;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Settings.CurrencySettings.Currency;
using WebApi.Modules.Settings.TaxSettings.TaxOption;
using WebApi.Modules.Settings.InventorySettings.InventoryStatus;

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
        [FwControllerMethod(Id:"kPJdOLE1mzSJ3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"b0WeUj4bhtKca", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair 
        [HttpGet]
        [FwControllerMethod(Id:"InQYyqMA8dBUA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<RepairLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<RepairLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/repair/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"aK7x7PAZ4fwCT", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<RepairLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<RepairLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair 
        [HttpPost]
        [FwControllerMethod(Id:"qd2ObhYdTAPPI", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<RepairLogic>> NewAsync([FromBody]RepairLogic l)
        {
            return await DoNewAsync<RepairLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/repair/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "aQiUmw2weALvD", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<RepairLogic>> EditAsync([FromRoute] string id, [FromBody]RepairLogic l)
        {
            return await DoEditAsync<RepairLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/repair/estimate/A0000001
        [HttpPost("estimate/{id}")]
        [FwControllerMethod(Id:"V6R1MLai1R7Fw", ActionType: FwControllerActionTypes.Option, Caption: "Estimate")]
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
        [FwControllerMethod(Id:"PgeX6is7sKrYI", ActionType: FwControllerActionTypes.Option, Caption: "Complete")]
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
        [FwControllerMethod(Id:"PpSdBovye5sNv", ActionType: FwControllerActionTypes.Option, Caption: "Release Items")]
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
        [FwControllerMethod(Id:"AxRbFcXeLZS0a", ActionType: FwControllerActionTypes.Option, Caption: "Void")]
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
        [FwControllerMethod(Id:"gT2Vr5OjOUHwK", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<RepairLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validateitem/browse
        [HttpPost("validateitem/browse")]
        [FwControllerMethod(Id: "tX5N53HrXg01", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateItemBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ItemLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validaterentalinventory/browse
        [HttpPost("validaterentalinventory/browse")]
        [FwControllerMethod(Id: "VOp20EULWvOq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRentalInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RentalInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatesalesinventory/browse
        [HttpPost("validatesalesinventory/browse")]
        [FwControllerMethod(Id: "6OO9FMaf4rnq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SalesInventoryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatedepartment/browse
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "y0XFqSkySp4M", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatedamageorder/browse
        [HttpPost("validatedamageorder/browse")]
        [FwControllerMethod(Id: "U8Nr63M7jORq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDamageOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OrderLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validaterepairitemstatus/browse
        [HttpPost("validaterepairitemstatus/browse")]
        [FwControllerMethod(Id: "oIUkgU6EB9D9", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRepairItemStatusBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RepairItemStatusLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validateofficelocation/browse
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "PpvdlqHRUcoH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatewarehouselocation/browse
        [HttpPost("validatewarehouselocation/browse")]
        [FwControllerMethod(Id: "8SjbOqgTjIsQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatecurrency/browse
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "7as7szmVFOLQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/repair/validatetaxoption/browse
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "HIX3g9Fmuomy", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTaxOptionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<TaxOptionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/repair/inventorystatus
        [HttpGet("inventorystatus")]
        [FwControllerMethod(Id: "XFzRtWE9367b", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<IEnumerable<InventoryStatusLogic>>> InventoryStatus_GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<InventoryStatusLogic>(pageno, pagesize, sort, typeof(InventoryStatusLogic));
        }
        //------------------------------------------------------------------------------------
    }
}
