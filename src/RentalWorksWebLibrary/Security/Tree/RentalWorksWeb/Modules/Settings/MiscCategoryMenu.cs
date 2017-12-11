using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class MiscCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MiscCategoryMenu() : base("{D5318A2F-ECB8-498A-9D9A-0846F4B9E4DF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{22B2E095-2B13-4AE8-98F7-4E4A594FA8A9}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{CBB393B3-DC58-4B97-A06F-0F8FDDFA1C27}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{708BA3A6-6730-4350-B909-46266FD965C7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3F1F2ABD-7274-4EFD-AA5E-F520E9666645}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{36EDB628-5A20-41CD-BF50-FC3B3F92309D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B4E09D2B-3183-42DF-BAF0-D1007F58BAD6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{0471C80B-E329-4FDC-8AD0-EFFD3209CFD8}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3D1332F2-DAF3-4179-AA8C-2F590BF78B8D}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{949D33B6-03E2-44D7-88D2-CAC550D21946}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4803D9C7-F21D-471D-87A9-8777DDFF9B8C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F28397EE-431E-48B3-8B0C-27112569511E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{2ED98ABD-E2FF-4EA9-83BD-CC880F634021}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{BBD9DC7F-327A-40AE-B2B0-4A53F6A4AADF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}