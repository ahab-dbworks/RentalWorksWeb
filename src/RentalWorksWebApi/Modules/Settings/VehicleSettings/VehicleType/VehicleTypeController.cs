using FwStandard.AppManager;
﻿using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.VehicleSettings.LicenseClass;
using WebApi.Modules.Settings.InventorySettings.Unit;
using WebApi.Modules.Settings.AccountingSettings.GlAccount;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleType
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "settings-v1")]
    [FwController(Id:"lbbSCJTjhBL3U")]
    public class VehicleTypeController : AppDataController
    {
        public VehicleTypeController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(VehicleTypeLogic); }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype/browse
        [HttpPost("browse")]
        [FwControllerMethod(Id:"hllL40yct9uS7", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"B9q87tZkPDNjd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype
        [HttpGet]
        [FwControllerMethod(Id:"CHjCU7JTKCkPV", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<VehicleTypeLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<VehicleTypeLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------
        // GET api/v1/vehicletype/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"yRdcEf24Uj8yE", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<VehicleTypeLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<VehicleTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/vehicletype
        [HttpPost]
        [FwControllerMethod(Id:"zV2HP24MJuC1E", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<VehicleTypeLogic>> NewAsync([FromBody]VehicleTypeLogic l)
        {
            return await DoNewAsync<VehicleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/vehicletyp/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "mCsleg27WLKvW", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<VehicleTypeLogic>> EditAsync([FromRoute] string id, [FromBody]VehicleTypeLogic l)
        {
            return await DoEditAsync<VehicleTypeLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // DELETE api/v1/vehicletype/A0000001
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4srmkJomyCHgA", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<VehicleTypeLogic>(id);
        }
        //------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateinventorytype/browse
        [HttpPost("validateinventorytype/browse")]
        [FwControllerMethod(Id: "SmLzxP6Hxbve", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validatelicenseclass/browse
        [HttpPost("validatelicenseclass/browse")]
        [FwControllerMethod(Id: "BFt83MHJxvSL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLicenseClassBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LicenseClassLogic>(browseRequest);
        }//------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateunit/browse
        [HttpPost("validateunit/browse")]
        [FwControllerMethod(Id: "DvGBAFQwhCBM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUnitBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UnitLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateassetaccount/browse
        [HttpPost("validateassetaccount/browse")]
        [FwControllerMethod(Id: "IqV8ovfOhFFs", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAssetAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }//------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateincomeaccount/browse
        [HttpPost("validateincomeaccount/browse")]
        [FwControllerMethod(Id: "6S56u2lyC95b", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validatesubincomeaccount/browse
        [HttpPost("validatesubincomeaccount/browse")]
        [FwControllerMethod(Id: "jEh2v878ePAM", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSubIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }//------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateequipmentsaleincomeaccount/browse
        [HttpPost("validateequipmentsaleincomeaccount/browse")]
        [FwControllerMethod(Id: "1JvcpeE8YoFQ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateEquipmentSaleIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validateldincomeaccount/browse
        [HttpPost("validateldincomeaccount/browse")]
        [FwControllerMethod(Id: "KTcptdlIvTYS", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLdIncomeAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }//------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validatecostofgoodssoldexpenseaccount/browse
        [HttpPost("validatecostofgoodssoldexpenseaccount/browse")]
        [FwControllerMethod(Id: "notjIGortsfT", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsSoldExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/vehicletype/validatecostofgoodsrentedexpenseaccount/browse
        [HttpPost("validatecostofgoodsrentedexpenseaccount/browse")]
        [FwControllerMethod(Id: "GgjyF7m49ArL", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCostOfGoodsRentedExpenseAccountBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GlAccountLogic>(browseRequest);
        }
    }
}
