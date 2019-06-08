using FwStandard.AppManager;
using WebApi.Logic;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using static WebApi.Modules.Home.InventorySearchPreview.InventorySearchPreviewController;

namespace WebApi.Modules.Home.InventorySearchPreview
{
    [FwLogic(Id:"JInOSUX6vwnT")]
    public class InventorySearchPreviewLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        protected InventorySearchPreviewRecord inventorySearch= new InventorySearchPreviewRecord();
        protected InventorySearchPreviewLoader inventorySearchLoader = new InventorySearchPreviewLoader();

        public InventorySearchPreviewLogic() : base()
        {
            dataRecords.Add(inventorySearch);
            dataLoader = inventorySearchLoader;

            ReloadOnSave = false;
            LoadOriginalBeforeSaving = false;
        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"cEmyG9vzCFKP", IsPrimaryKey:true)]
        public string Id { get { return inventorySearch.Id; } set { inventorySearch.Id = value; } }

        [FwLogicProperty(Id:"2RbZVy6pxUdB")]
        public string SessionId { get { return inventorySearch.SessionId; } set { inventorySearch.SessionId = value; } }

        [FwLogicProperty(Id:"3kb1jZvduWpJ")]
        public string ParentId { get { return inventorySearch.ParentId; } set { inventorySearch.ParentId = value; } }

        [FwLogicProperty(Id:"gP4EPlMaB5jj")]
        public string InventoryId { get { return inventorySearch.InventoryId; } set { inventorySearch.InventoryId = value; } }

        [FwLogicProperty(Id:"oEGSiGBusdOO")]
        public string WarehouseId { get { return inventorySearch.WarehouseId; } set { inventorySearch.WarehouseId = value; } }

        [FwLogicProperty(Id:"ZHcLSUe3362K")]
        public decimal? Quantity { get { return inventorySearch.Quantity; } set { inventorySearch.Quantity = value; } }

        //------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PreviewAsync(InventorySearchPreviewBrowseRequest request)
        {
            FwJsonDataTable dt = null;

            inventorySearchLoader.UserSession = this.UserSession;
            dt = await inventorySearchLoader.PreviewAsync(request);
            return dt;
        }
        //------------------------------------------------------------------------------------
    }
}
