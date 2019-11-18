using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PresentationLayerActivity
{
    public class SortActivitiesRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> PresentationLayerActivityIds { get; set; } = new List<string>();
    }

    public static class PresentationLayerActivityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortActivities(FwApplicationConfig appConfig, FwUserSession userSession, SortActivitiesRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "presentationlayeractivity";
            r2.IdFieldNames.Add("presentationlayeractivityid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.PresentationLayerActivityIds)
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

