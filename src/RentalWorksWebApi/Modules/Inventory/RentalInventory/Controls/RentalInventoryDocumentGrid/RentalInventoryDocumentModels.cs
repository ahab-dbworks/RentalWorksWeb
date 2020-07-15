using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Inventory.RentalInventory
{
    //*******************************************************************************************
    public class RentalInventoryDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            this.addFilterToSelect("InventoryId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class RentalInventoryDocumentLogic : AppDocumentLogic
    {
        RentalInventoryDocumentLoader documentLoader = new RentalInventoryDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public RentalInventoryDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "GFWd1dfyONg8")] 
        public string InventoryId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class RentalInventoryDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class RentalInventoryDocumentPutRequest : AppDocumentPutRequest
    {
        public string InventoryId { get; set; }
        
    }
    //*******************************************************************************************
    public class RentalInventoryDocumentPostRequest : AppDocumentPostRequest
    {
        public string InventoryId { get; set; }
    }
    //*******************************************************************************************
}
