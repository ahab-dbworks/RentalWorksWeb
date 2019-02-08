using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SingleRateWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SingleRateWarehouseGridMenu() : base("{0E4E4B5D-5905-4BD5-AC57-2DE047EFEB5B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D6A118BF-0F92-4983-888E-BFAAF867C873}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6B5D5C1D-3103-4F39-9D85-74E99640A70F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{5AC432F1-6B4B-4FD4-A5B1-91474803F800}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D581994C-C70D-459E-827B-C8D1D69EECA9}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{9F2BFE87-63B6-40F2-AFB3-428260F0665A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}