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
            if (string.IsNullOrEmpty(request.WarehouseId)) throw new ArgumentException($"WarehouseId is required. Value: ${request.WarehouseId}", "request.WarehouseId");
            if (string.IsNullOrEmpty(request.WarehouseId)) throw new ArgumentException($"ScannableMasterId is required. Value: {request.ScannableMasterId}", "request.ScannableMasterId");
            ContainerLogic containerLogic = FwBusinessLogic.CreateBusinessLogic<ContainerLogic>(this.AppConfig, this.UserSession);
            return await containerLogic.GetManyAsync<LookupContainerDescriptionResponse>(request, async (FwSqlSelect selectQry) =>
            {
                selectQry.AddWhere("warehouseid = @warehouseid");
                selectQry.AddWhere("availfor in ('R')");
                selectQry.AddWhere("availfrom in ('W')");
                selectQry.AddWhere("class in ('N')");
                selectQry.AddWhere("inactive <> 'T'");
                selectQry.AddWhere("scannablemasterid = @scannablemasterid");
                selectQry.AddParameter("@warehouseid", request.WarehouseId);
                selectQry.AddParameter("@scannablemasterid", request.ScannableMasterId);
                await Task.CompletedTask;
            });
        }
        //------------------------------------------------------------------------------------ 
    }
}
