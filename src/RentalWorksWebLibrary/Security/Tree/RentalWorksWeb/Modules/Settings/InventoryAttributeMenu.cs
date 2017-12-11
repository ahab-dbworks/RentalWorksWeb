using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryAttributeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeMenu() : base("{2777dd37-daca-47ff-aa44-29677b302745}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{d08f4d58-64ac-4231-91e4-3a804d26580c}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{a0990021-fd7c-4a16-a251-3f9155fca4e6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{c8f13f44-d598-4cfe-a64e-8dd441c24924}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{05e3023f-f05e-439f-882c-ce8b51dbc454}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{cc1be7df-3cc7-4a74-a3dd-c8cdaf9f62c1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{a53908cd-194a-4285-9189-afbd172d7e3c}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{ef9651fb-48ab-46cd-8c38-a8c84ce203de}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{983efcf3-d49a-4457-bc09-48de0eeef2ba}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{cb2e6a88-5024-4b51-981a-8081eb635a41}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{2a542752-fb62-437b-b73b-f8a6607e514c}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{11e0a790-a04b-4b56-aeac-ef3d63b230b9}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6c479779-aee0-412d-9754-0855481d2ecd}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{34fac46a-62bf-42fe-a084-cadb6816e8c4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}