using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Agent.PurchaseOrder
{
    //*******************************************************************************************
    public class PurchaseOrderDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            this.addFilterToSelect("PurchaseOrderId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class PurchaseOrderDocumentLogic : AppDocumentLogic
    {
        PurchaseOrderDocumentLoader documentLoader = new PurchaseOrderDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public PurchaseOrderDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string PurchaseOrderId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class PurchaseOrderDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class PurchaseOrderDocumentPutRequest : AppDocumentPutRequest
    {
        public string PurchaseOrderId { get; set; }
        
    }
    //*******************************************************************************************
    public class PurchaseOrderDocumentPostRequest : AppDocumentPostRequest
    {
        public string PurchaseOrderId { get; set; }
    }
    //*******************************************************************************************
}
