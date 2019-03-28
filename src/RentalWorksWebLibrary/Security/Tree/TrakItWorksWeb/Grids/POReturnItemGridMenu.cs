using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class POReturnItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnItemGridMenu() : base("{B5D69725-CB16-4E12-9457-1B6EB999FD41}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{632F3461-EE0F-437D-A8B8-A07802426FCA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{215B696C-4846-4D64-848F-D94919AB200D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{5E0506E4-82C2-46B3-B59B-F1308B35DAE3}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2253E0A8-5F27-4277-933B-A0B85AAAC4F7}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
