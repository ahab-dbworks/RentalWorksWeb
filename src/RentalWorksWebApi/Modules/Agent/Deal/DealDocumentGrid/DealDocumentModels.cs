using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Agent.Deal
{
    //*******************************************************************************************
    public class DealDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("DealId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class DealDocumentLogic : AppDocumentLogic
    {
        DealDocumentLoader documentLoader = new DealDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public DealDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string DealId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class DealDocumentPutRequest : AppDocumentPutRequest
    {
        public string DealId { get; set; }
        //public string DocumentId { get; set; }
        
    }
    //*******************************************************************************************
    public class DealDocumentPostRequest: AppDocumentPostRequest
    {
        public string DealId { get; set; }
        //public string DocumentTypeId { get; set; }
        //public string Description { get; set; }
        //public string FileDataUrl { get; set; }
        //public bool FileIsModified { get; set; }
        //public string FilePath { get; set; }
    }
    ////*******************************************************************************************
    //public class DealDocumemntGetRequest
    //{

    //}
    //*******************************************************************************************
}
