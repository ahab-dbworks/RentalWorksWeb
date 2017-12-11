using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class DocumentTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DocumentTypeMenu() : base("{358fbe63-83a7-4ab4-973b-1a5520573674}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{322b5dbc-d0ef-4583-90b2-65648a4d0907}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{511230cb-4b6d-4d94-90be-c046a6a0d9d6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{b3345869-b43e-4e70-afbe-8d1382fd7ccf}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7c5680d3-debd-466d-bf18-7a2dc2bfe6c2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{128351cc-8837-4962-8565-4edde0d15742}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{fe1e519a-c10f-4869-84db-0b82e29bff2b}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{1b5584d2-436a-425e-a10f-fad92697ce69}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4f4919d2-72dc-4523-a0c3-20c5cd6f24a6}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{24646cbb-e14a-4b1a-9626-22d04c5175c9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{2ae5e34a-a308-48ad-83d4-9bfc8916ef63}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{020da901-2d14-4358-8a43-1335bc7d5db0}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{f7b7ffcb-3881-457c-903a-850a3571ab98}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{fb4c9d81-df1f-4920-92f0-db99a0ed4f1f}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}