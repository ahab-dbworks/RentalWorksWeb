using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventoryPackageInventory
{
    public class SortInventoryPackageInventorysRequest
    {
        public string PackageId { get; set; }
        public int? StartAtIndex { get; set; }
        public List<string> InventoryPackageInventoryIds { get; set; } = new List<string>();
    }

    public static class InventoryPackageInventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortInventoryPackageInventorys(FwApplicationConfig appConfig, FwUserSession userSession, SortInventoryPackageInventorysRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "packageitem";
            r2.IdFieldNames.Add("packageid");
            r2.IdFieldNames.Add("packageitemid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;

            foreach (string itemId in request.InventoryPackageInventoryIds) {
                List<string> idCombo = new List<string>();
                idCombo.Add(request.PackageId);
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
    }
    //-------------------------------------------------------------------------------------------------------    
}

