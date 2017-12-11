using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class LaborCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public LaborCategoryMenu() : base("{2A5190B9-B0E8-4B93-897B-C91FC4807FA6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C2A0505E-B4BB-4A55-99DF-63016163AA3C}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8EE8BCA0-ABB9-45C3-90A1-1AEB4298EEDF}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5E3CF2F6-7886-412D-BAAF-6D098C85C7A1}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F12AB3EB-AFFD-4A1C-B330-9569E411D2CB}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1681937E-E4D1-44EB-9C05-B86BE8AA2DF4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{98E73E3C-F3A1-4030-99D0-4DC1FBD6CD6E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9BE3C149-783B-4412-8ADC-4B5EB995FF52}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E28DB5B6-6C81-479B-892D-08EE4A04F435}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4FD18E26-8977-45FB-AEFB-340B9226ECE5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CBB2618D-BE81-4642-9E55-1D4033C0691B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{0B8AE4EB-9262-4275-9B27-9DCC3A90D3A1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{89E98AFF-CBEB-4056-9B5E-2500156327F6}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CFF6AE0F-B3D3-4E34-98B8-3B63FC91CF71}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}