using FwStandard.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Settings.FacilitySettings.Building
{
    public class SortFloorsRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> FloorIds { get; set; } = new List<string>();
    }
    public class SortSpacesRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> SpaceIds { get; set; } = new List<string>();
    }
    public class SortSpaceRatesRequest
    {
        public int? StartAtIndex { get; set; }
        public List<string> SpaceRatesIds { get; set; } = new List<string>();
    }

    public static class BuildingFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortFloors(FwApplicationConfig appConfig, FwUserSession userSession, SortFloorsRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "floor";
            r2.IdFieldNames.Add("floorid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.FloorIds)
            {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<SortItemsResponse> SortSpaces(FwApplicationConfig appConfig, FwUserSession userSession, SortSpacesRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "master";
            r2.IdFieldNames.Add("masterid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.SpaceIds)
            {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
        //------------------------------------------------------------------------------------ 
        public static async Task<SortItemsResponse> SortSpaceRates(FwApplicationConfig appConfig, FwUserSession userSession, SortSpaceRatesRequest request)
        {
            SortItemsRequest r2 = new SortItemsRequest();
            r2.TableName = "spacrate";
            r2.IdFieldNames.Add("spacerateid");
            r2.RowNumberFieldName = "orderby";
            r2.StartAtIndex = request.StartAtIndex;
            r2.RowNumberDigits = 6;

            foreach (string itemId in request.SpaceRatesIds)
            {
                List<string> idCombo = new List<string>();
                idCombo.Add(itemId);
                r2.Ids.Add(idCombo);
            }
            SortItemsResponse response = await AppFunc.SortItems(appConfig, userSession, r2);
            return response;
        }
        //------------------------------------------------------------------------------------ 
    }
}
