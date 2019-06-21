using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class AvailabilitySettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AvailabilitySettingsMenu() : base("{E1C62A69-05B0-4657-AAF0-703F8BDEBC5C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{AD273C1B-31E5-46C6-8CBF-123F07A103A8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{29953B5D-807C-4618-AF9B-4CCF29F5F2C4}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{146900B8-9648-40F5-A7F5-19C3DC3DF8C4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{56F4D1B1-85DE-48D2-A05F-CB5546EE887E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7B4D2BCB-E92D-4C49-9808-021D9C2504F5}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{683E9ED9-A76B-4858-9E29-C60E9F838A6A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{8A9C95DD-4963-4F91-ACB0-05502209BD73}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{2FFAE426-D60F-47A8-B5F4-EE99D041BDED}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6208357B-7613-461E-BF17-23FC1E2133EA}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{14578B54-9EB0-415F-A8A0-EE6C16465894}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{3B19E66A-E551-4348-ABDB-133EAD148E6E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}




