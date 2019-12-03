using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApi.Modules.Utilities.DashboardSettings
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "utilities-v1")]
    [FwController(Id: "lXpomto7a29v")]
    public class DashboardSettingsController : AppDataController
    {
        public DashboardSettingsController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { logicType = typeof(DashboardSettingsLogic); }
        //------------------------------------------------------------------------------------
        // GET api/v1/dashboardsettings/A0000001
        [HttpGet("{id}")]
        [FwControllerMethod(Id:"c79UfgZ1Jat5a", ActionType: FwControllerActionTypes.Browse, ValidateSecurityGroup: false)]
        public async Task<ActionResult<DashboardSettingsLogic>> GetOneAsync([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DashboardSettingsLogic l = new DashboardSettingsLogic();
                l.SetDependencies(this.AppConfig, this.UserSession);
                await l.LoadAsync<DashboardSettingsLogic>(new object[] { id });
                return new OkObjectResult(l);
            }
            catch (Exception ex)
            {
                return GetApiExceptionResult(ex);
            }
        }
        //------------------------------------------------------------------------------------
        // POST api/v1/dashboardsettings 
        [HttpPost]
        [FwControllerMethod(Id:"c9DKCtKZ05H2T", ActionType: FwControllerActionTypes.New)]
        public async Task<ActionResult<DashboardSettingsLogic>> NewAsync([FromBody]DashboardSettingsLogic l)
        {
            l.SetDependencies(this.AppConfig, this.UserSession);
            return await DoPostAsync<DashboardSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------
        // PUT api/v1/dashboardsettings/A0000001
        [HttpPut("{id}")]
        [FwControllerMethod(Id: "vtZr1OXg1Dtcl", ActionType: FwControllerActionTypes.Edit)]
        public async Task<ActionResult<DashboardSettingsLogic>> EditAsync([FromRoute] string id, [FromBody]DashboardSettingsLogic l)
        {
            l.SetDependencies(this.AppConfig, this.UserSession);
            return await DoPostAsync<DashboardSettingsLogic>(l);
        }
        //------------------------------------------------------------------------------------ 
    }
}
