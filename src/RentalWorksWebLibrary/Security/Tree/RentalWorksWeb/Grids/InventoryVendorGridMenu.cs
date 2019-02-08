using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryVendorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryVendorGridMenu() : base("{C68281F9-0FC9-4FFE-8931-A5E501577AC3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8B70560E-3825-4881-A476-DE497D55638A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{7ABB2F1A-3225-40DA-B0A8-94CA916C37FB}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{597F58DA-4B57-4A85-9367-A19679D6C8B7}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{45AF0A64-02F8-47F5-88AB-3E4C7B2B34EC}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E466D765-11BE-4C81-BB67-8C95829CC817}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{981C9950-1EE6-4066-A6D3-F7706DD19921}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6479493E-2B5F-4AA5-AF0B-A306DDAAC8E8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}