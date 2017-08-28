using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ContactTitleMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactTitleMenu() : base("{1b9183b2-add9-416d-a5e1-59fe68104e4a}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{5fb31c7a-e112-4ca5-901f-c233870b4f63}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{f66fc9f8-fe7c-43ef-8c4b-a837a71328c2}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{432392dd-f6d8-406b-96a0-6b83c0ba967e}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7bb47c62-89cf-4ae0-afb3-434bdde42abc}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5964430d-8fef-4cea-868e-f0f5ac946725}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{94dfefb6-a0cd-4c97-872c-4fa59a55b47a}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{01ace4e8-a672-4841-b9db-188f2b29edd3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{908d3f4b-1053-443c-bf83-b28f389cb354}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{04b73ab7-8748-4c87-b7fa-c4215bb7b1e9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{401c4c4c-39b9-4974-aec5-9662b3ae6065}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{89ca77a0-4106-40c8-8b08-8bd0c135e729}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{4c1c40ff-d4cd-4516-b9a0-a1cab9969ee5}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8e5926ee-f16f-4551-8b75-23c3be902248}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}