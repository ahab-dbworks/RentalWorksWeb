using FwStandard.AppManager;
using FwStandard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Controllers;

namespace WebApi.Modules.Mobile.TransferIn
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "mobile-v1")]
    [FwController(Id:"zMaxj2QXfuaA")]
    public class TransferInController : AppDataController
    {
        public TransferInController(IOptions<FwApplicationConfig> appConfig) : base(appConfig) { }
        //------------------------------------------------------------------------------------ 
    }
}
