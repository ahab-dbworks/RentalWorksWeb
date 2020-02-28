using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.Category
{
    public class SortCategoryRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> CategoryIds { get; set; } = new List<string>();
    }

    public static class CategoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortCategory(FwApplicationConfig appConfig, FwUserSession userSession, SortCategoryRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "category";
            r2.IdFieldNames.Add("categoryid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.CategoryIds)
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

