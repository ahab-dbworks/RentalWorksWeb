using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class InventoryAttributeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeMenu() : base("{625A684E-DE81-4827-992F-B89B01671D38}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7CE47E2C-78E8-41FC-AFD4-06808D2F05C3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7C825FB4-433F-4DFE-867D-571B8995C8DD}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{99BFA2EF-F193-4877-AA11-2CF1A695C61A}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{98B5EE07-16EC-4DBB-AFA5-F80C1623A419}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{68579033-8CD7-4981-AF42-B45013E46ED9}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CF22F042-7065-4050-A31C-C5484F81C539}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0596AFC2-2509-4AE5-81B6-08470982DA87}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2641A282-7985-4716-9C1E-4F7B6107ABDC}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{60B3A800-25A0-45EB-8109-6E05C4B2C6C5}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F9CA2188-D38C-41F0-8CEF-78C9427E78D4}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2C680673-743F-4FE7-9E58-1B986C017607}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{5909D8FE-1B6D-4480-8600-A221B536A0E1}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{AF0CD358-EBFD-47C6-AC6C-7CFA4BC2D7CE}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
