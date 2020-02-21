using FwStandard.Data;
using FwStandard.Grids.AppDocument;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modules.Agent.Customer
{
    //*******************************************************************************************
    public class CustomerDocumentLoader : AppDocumentLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.addFilterToSelect("CustomerId", "uniqueid1", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
    //*******************************************************************************************
    public class CustomerDocumentLogic : AppDocumentLogic
    {
        CustomerDocumentLoader documentLoader = new CustomerDocumentLoader();
        //------------------------------------------------------------------------------------ 
        public CustomerDocumentLogic()
        {
            this.dataLoader = documentLoader;
        }
        public string CustomerId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
    }
    //*******************************************************************************************
    public class CustomerDocumentGetRequest : AppDocumentGetRequest
    {

    }
    //*******************************************************************************************
    public class CustomerDocumentPutRequest : AppDocumentPutRequest
    {
        public string CustomerId { get; set; }
    }
    //*******************************************************************************************
    public class CustomerDocumentPostRequest : AppDocumentPostRequest
    {
        public string CustomerId { get; set; }
    }
    //*******************************************************************************************
    //public class CustomerDocumentGetRequest
    //{

    //}
    //*******************************************************************************************
}
