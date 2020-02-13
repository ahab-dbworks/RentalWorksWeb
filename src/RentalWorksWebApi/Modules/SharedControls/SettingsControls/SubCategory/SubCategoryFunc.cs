using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.SubCategory
{
    public class SortSubCategoriesRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> SubCategoryIds { get; set; } = new List<string>();
    }

    public static class SubCategoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortSubCategories(FwApplicationConfig appConfig, FwUserSession userSession, SortSubCategoriesRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "subcategory";
            r2.IdFieldNames.Add("subcategoryid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.SubCategoryIds)
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

