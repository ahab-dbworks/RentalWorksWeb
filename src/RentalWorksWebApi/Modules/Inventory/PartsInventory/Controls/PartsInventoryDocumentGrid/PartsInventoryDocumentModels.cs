using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Inventory.PartsInventory
{
    //*******************************************************************************************
    public class PartsInventoryDocumentLoader : AppDocumentLoader
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
    public class PartsInventoryDocumentLogic : AppDocumentLogic
    {
        PartsInventoryDocumentLoader documentLoader = new PartsInventoryDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public PartsInventoryDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "a4qmEQ8V7Rrr")] 
        public string InventoryId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class PartsInventoryDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class PartsInventoryDocumentPutRequest : AppDocumentPutRequest
    {
        public string InventoryId { get; set; }
        
    }
    //*******************************************************************************************
    public class PartsInventoryDocumentPostRequest : AppDocumentPostRequest
    {
        public string InventoryId { get; set; }
    }
    //*******************************************************************************************
}
