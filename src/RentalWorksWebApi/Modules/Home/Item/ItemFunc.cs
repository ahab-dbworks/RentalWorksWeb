using FwStandard.Models;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.Item
{

    public class ItemByBarCodeResponse : TSpStatusReponse
    {
        public ItemLogic Item { get; set; } = new ItemLogic();
    }

    public static class ItemFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ItemByBarCodeResponse> GetByBarCode (FwApplicationConfig appConfig, FwUserSession userSession, string barCode)
        {
            ItemByBarCodeResponse response = new ItemByBarCodeResponse();

            string itemId = AppFunc.GetStringDataAsync(appConfig, "rentalitem", "barcode", barCode, "rentalitemid").Result;
            if (string.IsNullOrEmpty(itemId))
            {
                response.success = false;
                response.msg = $"Invalid Bar Code {barCode}";
            }
            else
            {
                ItemLogic l = new ItemLogic();
                l.SetDependencies(appConfig, userSession);
                l.ItemId = itemId;
                response.success = await l.LoadAsync<ItemLogic>();
                response.Item = l;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
