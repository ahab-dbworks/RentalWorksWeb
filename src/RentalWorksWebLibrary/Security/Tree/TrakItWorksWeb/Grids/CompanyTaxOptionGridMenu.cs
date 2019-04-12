using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    internal class CompanyTaxOptionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyTaxOptionGridMenu() : base("{87E47D86-C161-4337-8B0D-5BC9003E890E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{41B4977C-E663-4A50-86BF-46DF487868C7}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F3FDB1EA-9D7F-4628-9363-AFC6AE19038F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{DB529727-EA8B-4C67-9619-0B1C0CC7512B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{27E3CC62-5966-46A4-B96F-C386A7000758}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{0BB52747-9A6F-4B9B-BF32-7A23AC35AEE6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}