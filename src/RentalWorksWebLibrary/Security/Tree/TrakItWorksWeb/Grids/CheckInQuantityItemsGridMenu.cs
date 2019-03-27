using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckInQuantityItemsGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInQuantityItemsGrid() : base("{457BBDD6-B4B4-4671-B651-728A6ABF2BF0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{B4AE4D3F-0996-4FAD-B442-F79DCDF67EEE}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{519A8957-4694-4979-A8FF-9F1A81A10AF9}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8E2D4BE5-D880-449F-AF37-2CE846D04F7E}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D2A7B720-A99C-4F68-A842-A910D4BA56F0}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

