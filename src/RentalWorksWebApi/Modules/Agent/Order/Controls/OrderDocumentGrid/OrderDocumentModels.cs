using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Agent.Order
{
    //*******************************************************************************************
    public class OrderDocumentLoader : AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("OrderId", "uniqueid1", select, request);
            this.addFilterToSelect("UniqueId1", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class OrderDocumentLogic : AppDocumentLogic
    {
        OrderDocumentLoader documentLoader = new OrderDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public OrderDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string OrderId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class OrderDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class OrderDocumentPutRequest : AppDocumentPutRequest
    {
        public string OrderId { get; set; }
        
    }
    //*******************************************************************************************
    public class OrderDocumentPostRequest : AppDocumentPostRequest
    {
        public string OrderId { get; set; }
    }
    //*******************************************************************************************
}
