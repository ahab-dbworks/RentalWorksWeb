using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class HolidayMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public HolidayMenu() : base("{CFFEFF09-A083-478E-913C-945184B5DE94}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DD88F61D-5CD0-42B4-8AC8-35ED5759FBE1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{4FC1CB96-E589-427C-B6BB-0D67F9B3C2AC}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A0FD8FE2-F1AB-42D9-8F8F-AA05F15630B4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3AE92EFF-6FF3-4D84-BBDE-49E6F93985A2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{34B289BD-CD49-4F11-B414-ABA6465B4ED4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1EE23E93-86A8-49FF-A415-19C653897B58}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F1C5ECFB-C074-46EA-A506-ABE4A8612ABE}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{DE9A1A91-7130-4E87-948C-CA4DB3E67035}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{13726564-5830-45FA-9DA4-2D48984FD4AB}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{07D31984-E051-41F8-9B6C-9637B43ECBE2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{163A78C1-D3BA-4CC4-A55D-A9FA18D0D5E1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{E1DA7C43-234A-4F49-9429-06EF89E56211}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A0D6F95B-5CBB-4DFA-A4FB-BEFEF70B50B9}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}