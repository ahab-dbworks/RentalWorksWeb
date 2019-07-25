using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PhysicalInventoryCycleInventoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhysicalInventoryCycleInventoryGridMenu() : base("{CE9B28C8-D403-435D-842F-9DD5E6A2760A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D7B9321A-2699-4AB7-9265-4A2AF55D5C67}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{958C3F01-6EB5-4EDB-9A14-25910437C802}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{76A40A9E-F065-4EB8-AFF8-80DBBB56A069}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1D1F4B9B-4D6E-4BB6-9FBC-6E00B43766C3}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{E8D9FFC1-250A-4F52-A679-7CA2FE56AD33}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{117809AB-500B-4933-B621-929D82A68FB8}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5AD69E48-75E4-44C6-8B7A-080D59BA6DCF}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}