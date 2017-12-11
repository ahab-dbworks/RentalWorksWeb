using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWebApi
{
    public class WarehouseMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseMenu() : base("{997CF52E-BA31-44BA-A3A3-D8684FFFB15B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            tree.AddControllerMethod("Browse", "BrowseAsync", "{C6B7DAF2-E8AF-4FD6-B5A0-C81B170F0496}", MODULEID);
            tree.AddControllerMethod("GetMany", "GetAsync", "{3C07B074-8E90-4D73-948D-493BFC4C6333}", MODULEID);
            tree.AddControllerMethod("GetOne", "GetAsync", "{DCC0FCFF-0F5E-4D10-BE2C-3D7A75611224}", MODULEID);
            tree.AddControllerMethod("Insert/Update", "PostAsync", "{555E138F-2B70-4EFF-B5F6-D3A8AB22BF85}", MODULEID);
            tree.AddControllerMethod("Delete", "DeleteAsync", "{7595E293-CBFB-43E2-9186-CEE7BA3580C7}", MODULEID);
            tree.AddControllerMethod("ValidateDuplicate", "ValidateDuplicateAsync", "{A4B746C0-2E23-447E-898A-C750FC71F4D9}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}