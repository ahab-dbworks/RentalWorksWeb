using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Agent.Quote
{
    //*******************************************************************************************
    public class QuoteDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string QuoteId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("QuoteId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class QuoteDocumentLogic : AppDocumentLogic
    {
        QuoteDocumentLoader documentLoader = new QuoteDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public QuoteDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string QuoteId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class QuoteDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class QuoteDocumentPutRequest : AppDocumentPutRequest
    {
        public string QuoteId { get; set; }
        
    }
    //*******************************************************************************************
    public class QuoteDocumentPostRequest : AppDocumentPostRequest
    {
        public string QuoteId { get; set; }
    }
    //*******************************************************************************************
}
