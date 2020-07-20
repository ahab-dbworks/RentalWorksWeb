using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Warehouse.Contract
{
    //*******************************************************************************************
    public class ContractDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            //select.Parse();
            this.addFilterToSelect("ContractId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class ContractDocumentLogic : AppDocumentLogic
    {
        ContractDocumentLoader documentLoader = new ContractDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public ContractDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "lgDEsJeptXzB")] 
        public string ContractId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class ContractDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class ContractDocumentPutRequest : AppDocumentPutRequest
    {
        public string ContractId { get; set; }
        
    }
    //*******************************************************************************************
    public class ContractDocumentPostRequest : AppDocumentPostRequest
    {
        public string ContractId { get; set; }
    }
    //*******************************************************************************************
}
