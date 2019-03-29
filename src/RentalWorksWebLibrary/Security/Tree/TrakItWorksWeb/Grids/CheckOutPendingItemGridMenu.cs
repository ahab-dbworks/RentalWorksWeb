using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class CheckOutPendingItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckOutPendingItemGridMenu() : base("{560D1917-64B0-445D-9101-EED6D7C45811}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{37FD5D0E-7C15-4E19-9D9B-43AC41E6FF96}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{4525164B-9064-4E98-BA09-71723EDCCB74}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BCDA808B-7BA0-4250-B8F7-4BBA5EC1F448}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FFDA0B03-F5E8-42EC-9060-46FB30B16E87}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
