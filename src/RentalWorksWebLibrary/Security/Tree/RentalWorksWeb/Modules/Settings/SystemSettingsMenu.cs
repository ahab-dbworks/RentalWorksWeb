using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SystemSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SystemSettingsMenu() : base("{6EFC3A8C-E83E-4FE3-BAC8-8E04EBD7F204}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{762030C0-2C69-4DE7-A269-5C149E860B16}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{C2A4C7B0-7AB3-4BCB-9C12-F8B436A713E2}", nodeBrowse.Id);
            //var nodeBrowseSubMenu = tree.AddSubMenu("{C8C6DD27-79C0-4B8B-9464-E102ED86A388}", nodeBrowseMenuBar.Id);
            //    var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{DE9D33EB-26ED-4B53-B8AF-0A5207F35632}", nodeBrowseSubMenu.Id);
            tree.AddViewMenuBarButton("{3C006F9D-B00E-4B40-AAED-423EF459908A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{9AA37F47-0CD0-4148-900E-C161D5851D41}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0E0B5F5A-1B5D-4481-87AE-10EFE6B159C9}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{5FA85C80-9E5F-4A9D-B234-08D1C8E2FDE8}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{80829FC7-6F2B-4C45-9FB8-A78801F052CA}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{09BDF71D-3CF1-48A3-8921-D5E02D8E5025}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
