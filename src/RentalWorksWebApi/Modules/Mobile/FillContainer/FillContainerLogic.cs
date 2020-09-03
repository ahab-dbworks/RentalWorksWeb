using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Containers.Container;

namespace WebApi.Modules.Mobile.FillContainer
{
    public class FillContainerLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        public async Task<GetResponse<LookupContainerDescriptionResponse>> LookupContainerDescriptionAsync(LookupContainerDescriptionRequest request)
        {
            ContainerLogic containerLogic = FwBusinessLogic.CreateBusinessLogic<ContainerLogic>(this.AppConfig, this.UserSession);
            return await containerLogic.GetManyAsync<LookupContainerDescriptionResponse>(request);
        }
        //------------------------------------------------------------------------------------ 
    }
}
