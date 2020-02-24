using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Agent.Vendor
{
    //*******************************************************************************************
    public class VendorDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("VendorId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class VendorDocumentLogic : AppDocumentLogic
    {
        VendorDocumentLoader documentLoader = new VendorDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public VendorDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string VendorId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class VendorDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class VendorDocumentPutRequest : AppDocumentPutRequest
    {
        public string VendorId { get; set; }
        
    }
    //*******************************************************************************************
    public class VendorDocumentPostRequest : AppDocumentPostRequest
    {
        public string VendorId { get; set; }
    }
    //*******************************************************************************************
}
