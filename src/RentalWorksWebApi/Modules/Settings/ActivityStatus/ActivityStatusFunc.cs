using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.ActivityStatus
{
    public class SortActivityStatusRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> ActivityStatusIds { get; set; } = new List<string>();
    }

    public static class ActivityStatusFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortActivityStatus(FwApplicationConfig appConfig, FwUserSession userSession, SortActivityStatusRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "activitystatus";
            r2.IdFieldNames.Add("activitystatusid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;

            foreach (string itemId in request.ActivityStatusIds) {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
    }
    //-------------------------------------------------------------------------------------------------------    
}

