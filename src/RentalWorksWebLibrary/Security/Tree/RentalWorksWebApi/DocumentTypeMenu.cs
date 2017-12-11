using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class DocumentTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DocumentTypeMenu() : base("{343FD936-D5A3-420A-8B72-8A10248C39E4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{B1C7EC8C-A38B-4B88-A635-77106140632D}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{C819F03A-4EE9-482C-8157-B2477DA26DBF}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{64BC04C7-CE73-4124-A138-C51FC5BB064C}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{B13DD731-3955-4FA5-AD32-EE92994CAF7B}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{7EA7F5A5-CD3E-40E8-88B5-74690E33F0D8}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{276E8939-070A-4876-AE25-EF41C352C845}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}