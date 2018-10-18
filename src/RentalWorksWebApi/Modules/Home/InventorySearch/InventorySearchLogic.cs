using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using WebApi.Modules.Home.ItemDimension;
using WebApi.Modules.Home.Master;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using System.Threading.Tasks;
using FwStandard.SqlServer;

namespace WebApi.Modules.Home.InventorySearch
{
    public class InventorySearchLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected InventorySearchRecord inventorySearch = new InventorySearchRecord();
        protected InventorySearchLoader inventorySearchLoader = new InventorySearchLoader();

        public InventorySearchLogic() : base()
        {
            dataRecords.Add(inventorySearch);
            dataLoader = inventorySearchLoader;

            ReloadOnSave = false;
            LoadOriginalBeforeSaving = false;

        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SessionId { get { return inventorySearch.SessionId; } set { inventorySearch.SessionId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InventoryId { get { return inventorySearch.InventoryId; } set { inventorySearch.InventoryId = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseId { get { return inventorySearch.WarehouseId; } set { inventorySearch.WarehouseId = value; } }
        public string ParentId { get { return inventorySearch.ParentId; } set { inventorySearch.ParentId = value; } }
        public decimal? Quantity { get { return inventorySearch.Quantity; } set { inventorySearch.Quantity = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? TotalQuantityInSession { get { return inventorySearch.TotalQuantityInSession; } }

        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> SearchAsync(InventorySearchRequest request)
        {
            FwJsonDataTable dt = null;

            inventorySearchLoader.UserSession = this.UserSession;
            dt = await inventorySearchLoader.SearchAsync(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> SearchAccessoriesAsync(InventorySearchAccessoriesRequest request)
        {
            FwJsonDataTable dt = null;

            inventorySearchLoader.UserSession = this.UserSession;
            dt = await inventorySearchLoader.SearchAccessoriesAsync(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
        public async Task<InventorySearchGetTotalResponse> GetTotalAsync(InventorySearchGetTotalRequest request)
        {
            return await inventorySearch.GetTotalAsync(request);
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> AddToAsync(InventorySearchAddToRequest request)
        {
            return await inventorySearch.AddToAsync(request);
        }
        //------------------------------------------------------------------------------------
    }
}