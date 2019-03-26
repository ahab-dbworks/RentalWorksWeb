using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ExchangeItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeItemGridMenu() : base("{702D2298-6C34-4938-B97E-00ABACA7CA5C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{125E73FE-B408-49C3-A13E-953AF67D1045}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EB9BFE9B-7066-4EDF-B797-4D5AFE472B5A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FF1D4E0F-64D0-41E3-99FE-0442EC58CE9B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D99E58A5-33FD-4322-8CF1-2C4897F1BC47}", nodeBrowseOptions.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}
