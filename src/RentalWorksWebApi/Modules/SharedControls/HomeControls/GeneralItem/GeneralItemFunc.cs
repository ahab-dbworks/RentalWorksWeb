using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.GeneralItem
{
    public class SortGeneralItemRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> ItemIds { get; set; } = new List<string>();
    }

    public static class GeneralItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortGeneralItems(FwApplicationConfig appConfig, FwUserSession userSession, SortGeneralItemRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "master";
            r2.IdFieldNames.Add("masterid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.ItemIds)
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

