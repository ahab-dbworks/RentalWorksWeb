using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Mobile.ItemSetLocation
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"L8b4IMFGH6Vy")]
    public class AssetSetLocationController : AppDataController
    {
        public AssetSetLocationController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
