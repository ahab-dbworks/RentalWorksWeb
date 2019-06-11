using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class InventoryConditionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryConditionMenu() : base("{9CCB253D-3518-4908-9D20-CD82E01C9F9F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D6180748-669B-406F-BD78-57E9DF062F36}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0CE3DA78-C228-4860-9500-68B624DD5169}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{B61BC6E8-7C0F-401D-ADAD-FDEFD46C5EFD}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{8CEF90EA-7AC9-449D-8EF7-62BF94D5B501}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6B22A226-7813-4A24-A991-D898F1CB498D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1AD54643-2B63-4A06-A22F-77B9B69D0A3C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B8342C9A-7B3D-4994-9A24-588AF5815905}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D4862F40-97CB-4D88-94F8-62B083CFD956}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A2280E65-C454-4B1F-9480-16D0B105E0D6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{C316EF30-8CA6-49DF-B4CC-D3DA7186CF46}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{9F7F0C9E-8C51-49E9-81A9-D2DAE9DC77D8}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{32642965-65B1-4A85-95DD-F1587B9969BF}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{DFF24B35-F371-42FE-A37B-1F54A66793BE}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
