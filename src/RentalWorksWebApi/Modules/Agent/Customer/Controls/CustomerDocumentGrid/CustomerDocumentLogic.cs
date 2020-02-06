using FwStandard.Grids.AppDocument;

namespace WebApi.Modules.Agent.Customer
{
    public class CustomerDocumentLogic : AppDocumentLogic
    {
        string CustomerId { get { return this.appDocument.UniqueId1; } set { this.appDocument.UniqueId1 = value; } }
    }
}
