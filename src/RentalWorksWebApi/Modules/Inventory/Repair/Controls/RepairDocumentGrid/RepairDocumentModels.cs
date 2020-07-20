using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Inventory.Repair
{
    //*******************************************************************************************
    public class RepairDocumentLoader: AppDocumentLoader
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
    public class RepairDocumentLogic : AppDocumentLogic
    {
        RepairDocumentLoader documentLoader = new RepairDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public RepairDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "3l8QRjnZfrnJ")] 
        public string InventoryId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class RepairDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class RepairDocumentPutRequest : AppDocumentPutRequest
    {
        public string InventoryId { get; set; }
        
    }
    //*******************************************************************************************
    public class RepairDocumentPostRequest : AppDocumentPostRequest
    {
        public string InventoryId { get; set; }
    }
    //*******************************************************************************************
}
