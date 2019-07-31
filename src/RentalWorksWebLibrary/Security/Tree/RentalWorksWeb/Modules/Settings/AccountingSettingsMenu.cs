using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class AccountingSettingsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AccountingSettingsMenu() : base("{6EB6300F-1416-42DE-B776-3E418656021D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9B6F7D49-4BD3-40DB-8AA1-2683C79174F2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{EEFFA73F-356F-4F55-8BE5-43336B81C851}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{192348D0B-8943-4E2F-98D0-CD712250191D}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{60E8B59B-B2C8-47EA-8576-630A2C288E95}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DD5B9045-8CEC-424D-9A4D-E8CA768B3846}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{94A4A2A1-BF5F-4F67-AF24-C30585F041A9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B6BC89D6-F229-48CB-A2A8-F4A25849DA5D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{BD51F092-C221-4FC4-8991-A4247B43A364}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{705DFBE7-C5DC-4A4D-85CC-5A6D5142E4D9}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5E9359B6-C904-4F26-869B-56580EF1894E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{5C66C687-5000-4385-9228-BE6AA0580735}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}




