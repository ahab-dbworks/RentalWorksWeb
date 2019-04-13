using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobePeriodMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobePeriodMenu() : base("{BF51623D-ABA6-471A-BC00-4729067C64CF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4BBC2DE7-3655-465D-B71F-ABAE397B2642}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{155C2FBC-27F9-4CF4-A7EA-6B1A23B0891E}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{87CF8227-D629-4AB1-B804-6D6293A9402F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{19BDECFD-2EA9-4B90-A4BF-5788CDFE37EA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{32994AD5-532B-4FF4-935F-84D6EA632DA0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{CD666144-3C0C-4438-A900-A88450C7E26F}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4C941AA7-E7B8-4E30-A749-30D8C2452FC7}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2A1B5CD5-52F5-420F-8A7A-19E93D134C5A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C19D2123-A37F-457E-A65F-BFF58641C0B1}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7047CC3F-6023-4B81-8A51-5D8096E4F7DE}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{97881716-8F17-4009-BCB4-2F88646D8F3B}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{56011E4A-98C8-42FF-8608-6D7D8E42A4C2}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4E91E904-5B71-4406-98F9-9966211E2AFE}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}