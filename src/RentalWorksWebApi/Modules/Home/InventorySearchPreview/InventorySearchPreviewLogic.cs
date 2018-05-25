using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Modules.Home.ItemDimension;
using WebApi.Modules.Home.Master;
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using System.Threading.Tasks;
using FwStandard.SqlServer;
using WebApi.Modules.Home.InventorySearch;
using static WebApi.Modules.Home.InventorySearchPreview.InventorySearchPreviewController;

namespace WebApi.Modules.Home.InventorySearchPreview
{
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
        }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string Id { get { return inventorySearch.Id; } set { inventorySearch.Id = value; } }
        public string SessionId { get { return inventorySearch.SessionId; } set { inventorySearch.SessionId = value; } }
        public string ParentId { get { return inventorySearch.ParentId; } set { inventorySearch.ParentId = value; } }
        public string InventoryId { get { return inventorySearch.InventoryId; } set { inventorySearch.InventoryId = value; } }
        public string WarehouseId { get { return inventorySearch.WarehouseId; } set { inventorySearch.WarehouseId = value; } }
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