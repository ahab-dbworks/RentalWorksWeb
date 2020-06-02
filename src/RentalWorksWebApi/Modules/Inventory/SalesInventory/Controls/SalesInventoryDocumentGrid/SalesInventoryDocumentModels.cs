using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Inventory.SalesInventory
{
    //*******************************************************************************************
    public class SalesInventoryDocumentLoader : AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("InventoryId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class SalesInventoryDocumentLogic : AppDocumentLogic
    {
        SalesInventoryDocumentLoader documentLoader = new SalesInventoryDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public SalesInventoryDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "YB952hRnLRJY")] 
        public string InventoryId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class SalesInventoryDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class SalesInventoryDocumentPutRequest : AppDocumentPutRequest
    {
        public string InventoryId { get; set; }
        
    }
    //*******************************************************************************************
    public class SalesInventoryDocumentPostRequest : AppDocumentPostRequest
    {
        public string InventoryId { get; set; }
    }
    //*******************************************************************************************
}
