using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.GeneralItem
{
    public class SortGeneralItemRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> GeneralItemIds { get; set; } = new List<string>();
    }

    public static class GeneralItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortGeneralItems(FwApplicationConfig appConfig, FwUserSession userSession, SortGeneralItemRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "inventoryview";
            r2.IdFieldNames.Add("masterid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.GeneralItemIds)
            {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
    }
    //------------------------------------------------------------------------------------ 
}

