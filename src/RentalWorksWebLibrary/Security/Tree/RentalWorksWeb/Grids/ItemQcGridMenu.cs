using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ItemQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemQcGridMenu() : base("{496FEE6D-FC41-47D7-8576-7EF95CAE1B18}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F0863966-2736-46CD-B215-13D26B466FB2}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{2C92DD3A-BAAD-4CDD-A5B9-738638EE2969}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FFE5D225-76E9-4DA3-8F1B-9630BF9CB2A5}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3103C911-19E9-4CAF-9DE2-519220485C55}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}