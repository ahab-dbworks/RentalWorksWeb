using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class PoClassificationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PoClassificationMenu() : base("{65EE7A12-3879-4B4A-892E-AE1C204599F8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{5BE05141-F660-4B74-AAF2-3D20204F62E4}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{96EA3708-BFD3-4CF5-B124-0C2CAE683B41}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{4DC77892-B79D-4AF2-ACCC-D89F07CFD353}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{A0000AFF-9215-4494-9821-FBA3080BE6AF}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{D89DA038-AB2E-4E0A-B986-880E0A745BD9}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{5DBAEF9A-05CD-4EC7-9FC5-FDB90B3F2E51}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}