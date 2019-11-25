using FwStandard.AppManager;
using FwStandard.SqlServer;
using System.Collections.Generic;
using FwStandard.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Options; 
using WebApi.Controllers; 
using System.Threading.Tasks;
using WebApi.Modules.Administrator.Group;
using WebApi.Modules.Settings.ContactSettings.ContactTitle;
using WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation;
using WebApi.Modules.Settings.WarehouseSettings.Warehouse;
using WebApi.Modules.Settings.AddressSettings.State;
using WebApi.Modules.Settings.AddressSettings.Country;
using WebApi.Modules.Settings.CompanyDepartmentSettings.Department;
using WebApi.Modules.Settings.InventorySettings.InventoryType;
using WebApi.Modules.Settings.LaborSettings.LaborType;
using WebApi.Modules.Settings.MiscellaneousSettings.MiscType;
using WebApi.Modules.Settings.FacilitySettings.FacilityType;
using WebApi.Modules.Settings.UserSettings.Sound;


namespace WebApi.Modules.Administrator.User
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "administrator-v1")]
    [FwController(Id:"r1fKvn1KaFd0u")]
    public class UserController : AppDataController
    {
        public UserController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(UserLogic); }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/browse 
        [HttpPost("browse")]
        [FwControllerMethod(Id:"funlZdaTqF6fg", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> BrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/modulename/exportexcelxlsx
        [HttpPost("exportexcelxlsx")]
        [FwControllerMethod(Id:"0yn5pKJgieEge", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<DoExportExcelXlsxExportFileAsyncResult>> ExportExcelXlsxFileAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoExportExcelXlsxFileAsync(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user 
        [HttpGet]
        [FwControllerMethod(Id:"J1fKwMfRirqIj", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<IEnumerable<UserLogic>>> GetManyAsync([FromQuery]int pageno, [FromQuery]int pagesize, [FromQuery]string sort)
        {
            return await DoGetAsync<UserLogic>(pageno, pagesize, sort);
        }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/user/A0000001 
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"a39bfoeajNGtD", ActionType: FwControllerActionTypes.View)]
        public async Task<ActionResult<UserLogic>> GetOneAsync([FromRoute]string id)
        {
            return await DoGetAsync<UserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user 
        [HttpPost]
        [FwControllerMethod(Id:"hwJfXH8OWTYWf", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<UserLogic>> NewAsync([FromBody]UserLogic l)
        {
            return await DoNewAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // PUT api/v1/user/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "b1hVJqkdyadvj", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<UserLogic>> EditAsync([FromRoute] string id, [FromBody]UserLogic l)
        {
            return await DoEditAsync<UserLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
        // DELETE api/v1/user/A0000001 
        [HttpDelete("{id}")]
        [FwControllerMethod(Id:"4QiCY7sXmbJUS", ActionType: FwControllerActionTypes.Delete)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute]string id)
        {
            return await DoDeleteAsync<UserLogic>(id);
        }
        //------------------------------------------------------------------------------------ 
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validategroup/browse 
        [HttpPost("validategroup/browse")]
        [FwControllerMethod(Id: "pMRJstqe4MUJ", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateGroupBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<GroupLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validateusertitle/browse 
        [HttpPost("validateusertitle/browse")]
        [FwControllerMethod(Id: "kNWIPr4j6KVv", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateUserTitleBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<ContactTitleLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validateofficelocation/browse 
        [HttpPost("validateofficelocation/browse")]
        [FwControllerMethod(Id: "VAbck19CXz7R", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateOfficeLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<OfficeLocationLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatewarehouselocation/browse 
        [HttpPost("validatewarehouselocation/browse")]
        [FwControllerMethod(Id: "6jd9Paf2dqKh", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateWarehouseLocationBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<WarehouseLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatestate/browse 
        [HttpPost("validatestate/browse")]
        [FwControllerMethod(Id: "4V7sCcBO1wW2", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateStateBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<StateLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatecountry/browse 
        [HttpPost("validatecountry/browse")]
        [FwControllerMethod(Id: "rn4cMsUUPfMH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateCountryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<CountryLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validaterentaldepartment/browse 
        [HttpPost("validaterentaldepartment/browse")]
        [FwControllerMethod(Id: "n5C6E3sGY4jz", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRentalDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatesalesdepartment/browse 
        [HttpPost("validatesalesdepartment/browse")]
        [FwControllerMethod(Id: "4RaV7t1JvkRD", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatelabordepartment/browse 
        [HttpPost("validatelabordepartment/browse")]
        [FwControllerMethod(Id: "gdpUE0BXjzeH", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatemiscdepartment/browse 
        [HttpPost("validatemiscdepartment/browse")]
        [FwControllerMethod(Id: "JFk7drdxdEh5", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMiscDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatepartsdepartment/browse 
        [HttpPost("validatepartsdepartment/browse")]
        [FwControllerMethod(Id: "s9Wxpcnbn9YA", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePartsDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatefacilitydepartment/browse 
        [HttpPost("validatefacilitydepartment/browse")]
        [FwControllerMethod(Id: "OHb8DvqFNCnC", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateFacilityDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatetransportationdepartment/browse 
        [HttpPost("validatetransportationdepartment/browse")]
        [FwControllerMethod(Id: "j3GdhvzL5uCF", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransportationDepartmentBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<DepartmentLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validaterentalinventory/browse 
        [HttpPost("validaterentalinventory/browse")]
        [FwControllerMethod(Id: "ceJgncuJPTsG", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateRentalInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatesalesinventorytype/browse 
        [HttpPost("validatesalesinventorytype/browse")]
        [FwControllerMethod(Id: "pe3SpMr0Wvov", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSalesInventoryBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatepartsinventorytype/browse 
        [HttpPost("validatepartsinventorytype/browse")]
        [FwControllerMethod(Id: "Gj42KoFueG5z", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidatePartsInventoryTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatetransportationtype/browse 
        [HttpPost("validatetransportationtype/browse")]
        [FwControllerMethod(Id: "UjsKhMVzK9Yd", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateTransportationTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<InventoryTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatelabortype/browse 
        [HttpPost("validatelabortype/browse")]
        [FwControllerMethod(Id: "BQwscMSR0oKe", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateLaborTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<LaborTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatemisctype/browse 
        [HttpPost("validatemisctype/browse")]
        [FwControllerMethod(Id: "jC9Cp8CSjE7g", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateMiscTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<MiscTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatefacilitytype/browse 
        [HttpPost("validatefacilitytype/browse")]
        [FwControllerMethod(Id: "vQRlIYTHt1B3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateFacilityTypeBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<FacilityTypeLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatesuccesssound/browse 
        [HttpPost("validatesuccesssound/browse")]
        [FwControllerMethod(Id: "IwclpYjT308Y", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateSuccessSoundBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SoundLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validateerrorsound/browse 
        [HttpPost("validateerrorsound/browse")]
        [FwControllerMethod(Id: "FtycWRuy8go3", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateErrorSoundBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SoundLogic>(browseRequest);
        }
        //------------------------------------------------------------------------------------ 
        // POST api/v1/user/validatenotificationsound/browse 
        [HttpPost("validatenotificationsound/browse")]
        [FwControllerMethod(Id: "8FK7IMLCjH7P", ActionType: FwControllerActionTypes.Browse)]
        public async Task<ActionResult<FwJsonDataTable>> ValidateNotificationSoundBrowseAsync([FromBody]BrowseRequest browseRequest)
        {
            return await DoBrowseAsync<SoundLogic>(browseRequest);
        }

    }
}
