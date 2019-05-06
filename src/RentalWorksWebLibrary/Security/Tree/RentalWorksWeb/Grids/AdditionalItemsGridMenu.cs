using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class AdditionalItemsGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public AdditionalItemsGrid() : base("{C9AAA0E7-466E-47F1-973D-61555FFCA6B8}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{2110F61A-8EF9-4AF4-8A81-884B49F8B4D3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{5A76AF0E-E4ED-4230-948A-5D2CE08CFFAF}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{94F7C958-9F48-47C3-9724-FA5F6A47B25B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BC3C3F26-BF35-4E3E-8E7C-65D15C5F29CD}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{30268D17-208A-4AF3-9C6A-A430FC6C701F}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{8AAFC7BA-7979-4490-88F2-188593DDE134}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1B7A208B-5B06-42E6-ABA9-7328115105AA}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}