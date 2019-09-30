using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.OrderTypeDateType
{
    public class SortOrderTypeDateTypesRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> OrderTypeDateTypeIds { get; set; } = new List<string>();
    }

    public static class OrderTypeDateTypeFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortOrderTypeDateTypes(FwApplicationConfig appConfig, FwUserSession userSession, SortOrderTypeDateTypesRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "ordertypedatetype";
            r2.IdFieldNames.Add("ordertypedatetypeid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;

            foreach (string itemId in request.OrderTypeDateTypeIds) {
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

