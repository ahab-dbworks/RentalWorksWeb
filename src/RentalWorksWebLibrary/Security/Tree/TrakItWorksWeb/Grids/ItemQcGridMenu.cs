using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ItemQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemQcGridMenu() : base("{63A92198-F5D0-4A9C-AAF5-08BF052A1CAA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{846A570B-2AAF-4E95-B975-7C3D3BC6792F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F619813A-49B2-4329-83BD-0CE929759A3C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D9D09750-1D39-4519-8D7E-F5E0A0374F89}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FE78EAC0-19C7-4066-9957-7C319654B813}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
