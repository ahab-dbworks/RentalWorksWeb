using FwStandard.AppManager;
using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modules.Agent.Project
{
    //*******************************************************************************************
    public class ProjectDocumentLoader: AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("ProjectId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class ProjectDocumentLogic : AppDocumentLogic
    {
        ProjectDocumentLoader documentLoader = new ProjectDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public ProjectDocumentLogic() : base()
        {
            this.dataLoader = documentLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Adj9a4VuxAol")] 
        public string ProjectId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class ProjectDocumentGetRequest : AppDocumentGetRequest
    {
        
    }
    //*******************************************************************************************
    public class ProjectDocumentPutRequest : AppDocumentPutRequest
    {
        public string ProjectId { get; set; }
        
    }
    //*******************************************************************************************
    public class ProjectDocumentPostRequest : AppDocumentPostRequest
    {
        public string ProjectId { get; set; }
    }
    //*******************************************************************************************
}
