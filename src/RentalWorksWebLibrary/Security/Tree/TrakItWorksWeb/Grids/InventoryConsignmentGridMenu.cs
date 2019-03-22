using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryConsignmentGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryConsignmentGridMenu() : base("{2DC8659F-EB2B-43C3-BAD1-0769EB14351F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D8D7B2E6-88BA-48F9-A820-B9EA9E99B2BA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EBFCBE8E-B0A4-47FC-B10F-A2336F938E66}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D683A7A9-9FB2-4DD7-A175-0251621DBFE1}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EAE44ABD-CFAF-4F3E-8F2E-2875F614047C}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
