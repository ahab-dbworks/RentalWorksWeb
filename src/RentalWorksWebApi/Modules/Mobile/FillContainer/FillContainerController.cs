using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Modules.Containers.Container;

namespace WebApi.Modules.Mobile.FillContainer
{
    [Route("api/v1/quikscan/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"t4YTMXX0TFgw")]
    public class FillContainerController : AppDataController
    {
        public FillContainerController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
        // GET api/v1/quikscan/fillcontainer/lookupcontainerdescription
        /// <summary>
        /// Get a list of valid Container Descriptions.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("lookupcontainerdescription")]
        [FwControllerMethod(Id: "qc3ayjuTNb6h", FwControllerActionTypes.Browse)]
        public async Task<ActionResult<GetResponse<LookupContainerDescriptionResponse>>> LookupContainerDescriptionAsync([FromQuery] LookupContainerDescriptionRequest request)
        {
            FillContainerLogic fillContainerLogic = FwBusinessLogic.CreateBusinessLogic<FillContainerLogic>(this.AppConfig, this.UserSession);
            return Ok(await fillContainerLogic.LookupContainerDescriptionAsync(request));
        }
        //------------------------------------------------------------------------------------ 
    }
}
