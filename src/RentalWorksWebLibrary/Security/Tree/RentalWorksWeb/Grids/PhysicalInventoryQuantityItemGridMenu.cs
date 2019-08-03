using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class PhysicalInventoryQuantityItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhysicalInventoryQuantityItemGridMenu() : base("{7AA9683E-EEF1-4437-87BD-D0E137CD8506}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F7B97EA5-3AF9-4A03-A871-0E62AA80F72C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{838DE12B-3D6B-41E6-BF4F-D2F9D177F365}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{DA25ED74-9A78-4732-9702-D01955002A9F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DB3C3370-7896-4112-B1F4-B579E72A27E1}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
