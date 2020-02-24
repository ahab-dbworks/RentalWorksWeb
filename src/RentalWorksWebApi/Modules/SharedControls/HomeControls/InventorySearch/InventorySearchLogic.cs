using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Modules.HomeControls.ItemDimension;
using WebApi.Modules.HomeControls.Master;
using WebApi.Logic;
using static FwStandard.Data.FwDataReadWriteRecord;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using System;

namespace WebApi.Modules.HomeControls.InventorySearch
{
    [FwLogic(Id:"FBvu8xJVylI4")]
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
        [FwLogicProperty(Id:"1QMq6pJO92DC", IsPrimaryKey:true)]
        public string SessionId { get { return inventorySearch.SessionId; } set { inventorySearch.SessionId = value; } }

        [FwLogicProperty(Id:"mQT7dLv4mKg6", IsPrimaryKey:true)]
        public string InventoryId { get { return inventorySearch.InventoryId; } set { inventorySearch.InventoryId = value; } }

        [FwLogicProperty(Id:"qSFRDKnn1Dnv", IsPrimaryKey:true)]
        public string WarehouseId { get { return inventorySearch.WarehouseId; } set { inventorySearch.WarehouseId = value; } }

        [FwLogicProperty(Id:"s66OgVvyAPW4")]
        public string ParentId { get { return inventorySearch.ParentId; } set { inventorySearch.ParentId = value; } }

        [FwLogicProperty(Id: "7Iz1MCtA40NsH")]
        public string GrandParentId { get { return inventorySearch.GrandParentId; } set { inventorySearch.GrandParentId = value; } }

        [FwLogicProperty(Id:"UG7rknSx0rbh")]
        public float? Quantity { get { return inventorySearch.Quantity; } set { inventorySearch.Quantity = value; } }

        [FwLogicProperty(Id: "A31rHKgRnqgGv")]
        public DateTime? FromDate { get { return inventorySearch.FromDate; } set { inventorySearch.FromDate = value; } }

        [FwLogicProperty(Id: "tfKPrmyQwO1J0")]
        public DateTime? ToDate { get { return inventorySearch.ToDate; } set { inventorySearch.ToDate = value; } }
        
        [FwLogicProperty(Id: "oQb7QncbU0rA1")]
        public float? QuantityAvailable { get { return inventorySearch.QuantityAvailable; } }

        [FwLogicProperty(Id: "63DshaJDHrYHw")]
        public DateTime? ConflictDate { get { return inventorySearch.ConflictDate; } }

        [FwLogicProperty(Id: "AkWX42DcxB5q8")]
        public string AvailabilityState { get { return inventorySearch.AvailabilityState; } }

        [FwLogicProperty(Id: "Ck2zYEVcS3MfD")]
        public float? QuantityAvailableAllWarehouses { get { return inventorySearch.QuantityAvailableAllWarehouses; } }

        [FwLogicProperty(Id: "i8wNFe1g1tnEu")]
        public DateTime? ConflictDateAllWarehouses { get { return inventorySearch.ConflictDateAllWarehouses; } }

        [FwLogicProperty(Id: "WnTDyuzwE4OH4")]
        public string AvailabilityStateAllWarehouses { get { return inventorySearch.AvailabilityStateAllWarehouses; } }


        [FwLogicProperty(Id:"tvd6AqBMrgc4", IsReadOnly:true)]
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
        //public async Task<InventorySearchGetTotalResponse> GetTotalAsync(InventorySearchGetTotalRequest request)
        //{
        //    return await inventorySearch.GetTotalAsync(request);
        //}
        ////------------------------------------------------------------------------------------
        //public async Task<bool> AddToAsync(InventorySearchAddToRequest request)
        //{
        //    return await inventorySearch.AddToAsync(request);
        //}
        ////------------------------------------------------------------------------------------
    }
}
