using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.RegionSettings.Region;
using WebApi.Modules.Settings.CurrencySettings.Currency;
using WebApi.Modules.Settings.InventorySettings.BarCodeRange;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Agent.Deal;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;

namespace WebApi.Modules.Settings.WarehouseSettings.Warehouse
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"ICJcR2gOu04OB")]
    public class WarehouseController : AppDataController
    {
        public WarehouseController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(WarehouseLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"ddZHw4KQLyUAf", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouse/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"ppVJpliuTuIgs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/warehouse
        [HttpGet]
        [FwControllerMethod(Id:"C6JqXCViTUJ3g", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<WarehouseLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<WarehouseLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/warehouse/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"3XXkOAC0PQtRD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<WarehouseLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<WarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse
        [HttpPost]
        [FwControllerMethod(Id:"o3XDtND7MjAJM", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<WarehouseLogic>> NewAsync([FromBody]WarehouseLogic l)
        {
            return await DoNewAsync<WarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/warehouse/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "h1Ih7WjKGX8gY", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<WarehouseLogic>> EditAsync([FromRoute] string id, [FromBody]WarehouseLogic l)
        {
            return await DoEditAsync<WarehouseLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/warehouse/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"bZyYMoAKPXwtF", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<WarehouseLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validatecountry/browse
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "dXbQqJbMysbF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validateregion/browse
        [HttpPost("validateregion/browse")]
        [FwControllerMethod(Id: "2OMYspMW4jIr", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRegionBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<RegionLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validatecurrency/browse
        [HttpPost("validatecurrency/browse")]
        [FwControllerMethod(Id: "gmmuQtnfuxr1", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCurrencyBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CurrencyLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouse/validaterentalbarcoderange/browse
        [HttpPost("validaterentalbarcoderange/browse")]
        [FwControllerMethod(Id: "dcVnLtjo9DwR", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRentalBarCodeRangeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BarCodeRangeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validatesalesbarcoderange/browse
        [HttpPost("validatesalesbarcoderange/browse")]
        [FwControllerMethod(Id: "tsaVWg14pUKe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesBarCodeRangeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BarCodeRangeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validaterentalfixedassetbarcoderange/browse
        [HttpPost("validaterentalfixedassetbarcoderange/browse")]
        [FwControllerMethod(Id: "xR5ef5mExUVq", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRentalFixedAssetBarcodeRangeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<BarCodeRangeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/warehouse/validateinternalvendor/browse
        [HttpPost("validateinternalvendor/browse")]
        [FwControllerMethod(Id: "DzkK9urMYEbz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInternalVendorBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validateinternaldeal/browse
        [HttpPost("validateinternaldeal/browse")]
        [FwControllerMethod(Id: "sBtnGwMtCQY4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInternalDealBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DealLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/warehouse/validatetaxoption/browse
        [HttpPost("validatetaxoption/browse")]
        [FwControllerMethod(Id: "xdjds1uHFUsx", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
    }
}
