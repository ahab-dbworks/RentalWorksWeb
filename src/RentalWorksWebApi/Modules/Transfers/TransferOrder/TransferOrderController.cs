using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.AppManager;
using Microsoft.AspNetCore.Http;
using System;
using WebApi.Modules.Settings.DepartmentSettings.Department;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Administrator.User;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.Settings.ShipViaSettings.ShipVia;
using WebApi.Modules.Settings.AddressSettings.Country;

namespace WebApi.Modules.Transfers.TransferOrder
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "home-v1")]
    [FwController(Id: "tWkLbjsVHH6N")]
    public class TransferOrderController : AppDataController
    {
        public TransferOrderController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(TransferOrderLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id: "KoavNP3n6KyX", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id: "jDtpNIks5ABY", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------    
        // POST api/v1/transferorder/confirm/A0000001
        [HttpPost("confirm/{id}")]
        [FwControllerMethod(Id: "VHP1qrNmwB4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<TransferOrderLogic>> ConfirmTransferOrder([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string[] ids = id.Split('~');
                TransferOrderLogic transfer = new TransferOrderLogic();
                transfer.SetDependencies(AppConfig, UserSession);
                if (await transfer.LoadAsync<TransferOrderLogic>(ids))
                {
                    await TransferOrderFunc.ConfirmTransfer(AppConfig, UserSession, id);
                    return new OkObjectResult(transfer);
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
        // GET api/v1/transferorder 
        [HttpGet]
        [FwControllerMethod(Id: "p6yB4Twtje2S", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<TransferOrderLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<TransferOrderLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/transferorder/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id: "CHin19W7EJGg", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<TransferOrderLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<TransferOrderLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder 
        [HttpPost]
        [FwControllerMethod(Id: "8BWHylwYTScW", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<TransferOrderLogic>> NewAsync([FromBody]TransferOrderLogic l)
        {
            return await DoNewAsync<TransferOrderLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/transferorder/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "2dAQm1po5ECVB", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<TransferOrderLogic>> EditAsync([FromRoute] string id, [FromBody]TransferOrderLogic l)
        {
            return await DoEditAsync<TransferOrderLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        //// DELETE api/v1/transferorder/A0000001 
        //[HttpDelete("{id}")]
        //[FwControllerMethod(Id: "EqJ7cN287WIb", ActionType: FwControllerActionTypes.Delete)]
        //public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        //{
        //    return await DoDeleteAsync<TransferOrderLogic>(id);
        //}
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validatedepartment/browse 
        [HttpPost("validatedepartment/browse")]
        [FwControllerMethod(Id: "tMO0u5ONPVU7", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransferOrderBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validatefromwarehouse/browse 
        [HttpPost("validatefromwarehouse/browse")]
        [FwControllerMethod(Id: "6gWw6xtYWgmu", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateFromWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validatetowarehouse/browse 
        [HttpPost("validatetowarehouse/browse")]
        [FwControllerMethod(Id: "cJOltqawuGAt", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateToWarehouseBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validateagent/browse 
        [HttpPost("validateagent/browse")]
        [FwControllerMethod(Id: "YbeY2ItrJ0St", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateAgentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validateoutdeliverycarrier/browse 
        [HttpPost("validateoutdeliverycarrier/browse")]
        [FwControllerMethod(Id: "MW9jtyAmAuWU", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryCarrierBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<VendorLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validateoutdeliveryshipvia/browse 
        [HttpPost("validateoutdeliveryshipvia/browse")]
        [FwControllerMethod(Id: "qFvAVzMzPDG4", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOutDeliveryShipViaBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ShipViaLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validateoutdeliverytocountry/browse 
        [HttpPost("validateoutdeliverytocountry/browse")]
        [FwControllerMethod(Id: "tE6oX5Un0wGG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateDeliveryToCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/transferorder/validatesendto/browse 
        [HttpPost("validatesendto/browse")]
        [FwControllerMethod(Id: "1nsCXoIHInCP", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSendToBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<UserLogic>(browseRequest);
        }
    }
}
